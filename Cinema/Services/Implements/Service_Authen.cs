using Cinema.Entities;
using Cinema.Handle;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO.Users_DTO;
using Cinema.Payloads.Requests.Users_RQ;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;
using SmtpClient = System.Net.Mail.SmtpClient;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text;
using System.Security.Cryptography;
using BCryptNet = BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinema.Payloads.Requests.InputRequests;
using Cinema.DataContext;


namespace Cinema.Services.Implements
{
    public class Service_Authen : IService_Authen
    {
        private readonly ResponseObject<DTO_DangKi> responseObject;
        private readonly Converter_DangKi converter;
        private readonly IConfiguration _configuration;
        private readonly ResponseObject<DTO_Token> _responseObjectToken;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext dbContext;
        public int Idtempt;
        public Service_Authen(IConfiguration configuration, ResponseObject<DTO_Token> responseObjectToken,ResponseObject<DTO_DangKi> _reponseObject, IHttpContextAccessor httpContextAccessor, AppDbContext appDbContext, Converter_DangKi converter_DangKi)
        {


            _configuration = configuration;
            responseObject = _reponseObject;
            converter = converter_DangKi;
            _responseObjectToken = responseObjectToken;
            _httpContextAccessor = httpContextAccessor;
            dbContext = appDbContext;
        }
        #region GenerateRefreshToken
        public string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        #endregion
        #region GenerateAccessToken
        public DTO_Token GenerateAccessToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);

            var decentralization = dbContext.roles.FirstOrDefault(x => x.Id == user.RoleId);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Username", user.Username),
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim(ClaimTypes.Role, decentralization?.Code ?? "")
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            RefreshToken rf = new RefreshToken
            {
                Token = refreshToken,
                ExpiredTime = DateTime.Now.AddHours(4),
                UserId = user.Id
            };

            dbContext.refreshTokens.Add(rf);
            dbContext.SaveChanges();

            DTO_Token tokenDTO = new DTO_Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return tokenDTO;
        }
        #endregion
        #region RenewAccessToken
        public ResponseObject<DTO_Token> RenewAccessToken(DTO_Token request)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);

            var tokenValidation = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value))
            };

            try
            {
                var tokenAuthentication = jwtTokenHandler.ValidateToken(request.AccessToken, tokenValidation, out var validatedToken);
                if (validatedToken is not JwtSecurityToken jwtSecurityToken || jwtSecurityToken.Header.Alg != SecurityAlgorithms.HmacSha256)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Token không hợp lệ", null);
                }
                RefreshToken refreshToken = dbContext.refreshTokens.FirstOrDefault(x => x.Token == request.RefreshToken);
                if (refreshToken == null)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "RefreshToken không tồn tại trong database", null);
                }
                if (refreshToken.ExpiredTime < DateTime.Now)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status401Unauthorized, "Token chưa hết hạn", null);
                }
                var user = dbContext.users.FirstOrDefault(x => x.Id == refreshToken.UserId);
                if (user == null)
                {
                    return _responseObjectToken.ResponseError(StatusCodes.Status404NotFound, "Người dùng không tồn tại", null);
                }
                var newToken = GenerateAccessToken(user);

                return _responseObjectToken.ResponseSuccess("Làm mới token thành công", newToken);
            }
            catch (Exception ex)
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status500InternalServerError, ex.Message, null);
            }
        }
        #endregion
        #region  SendEmail
        public string SendEmail(EmailTo emailTo)
        {

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("buiminhthucvv2002@gmail.com", "rhum rqpf hvvm pbca"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("buiminhthucvv2002@gmail.com");
                message.To.Add(emailTo.Mail);
                message.Subject = emailTo.Subject;
                message.Body = emailTo.Content;
                message.IsBodyHtml = true;
                smtpClient.Send(message);

                return "Gửi email thành công";
            }
            catch (Exception ex)
            {
                return "Lỗi khi gửi email: " + ex.Message;
            }
        }
        #endregion
        #region DangKi 
        public ResponseObject<DTO_DangKi> DangKi(Request_DangKi request)
        {
            if (!Email.IsValiEmail(request.Email))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Định dạng Email không hợp lệ !", null);
            if (string.IsNullOrWhiteSpace(request.Email))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền Email !", null);
            if (string.IsNullOrWhiteSpace(request.Name))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền Name !", null);
            if (string.IsNullOrWhiteSpace(request.Username))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền Username !", null);
            if (string.IsNullOrWhiteSpace(request.Password))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền Password !", null);
            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền PhoneNumber !", null);
            Regex regex = new Regex("[!@#$%^&*()_+{}\\[\\]:;<>,.?/~`]");
            if (request.Password.Length < 8 || request.Password.Length > 20 || !regex.IsMatch(request.Password))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Password phải từ 8-20 kí tự và chứa ít nhất 1 kí tự đặc biệt !", null);
            EmailTo emailTo = new EmailTo();
            emailTo.Mail = request.Email;
            emailTo.Subject = "XÁC NHẬN EMAIL";
            Random random = new Random();
            int otp = random.Next(100000, 1000000);
            emailTo.Content = "Mã otp của bạn là:" + otp + "\n Mã xác nhận của bạn sẽ hết hạn sau 5 phút nữa !";
            SendEmail(emailTo);
            var user = new User();
            user.Username = request.Username;
            user.Password = BCryptNet.HashPassword(request.Password);
            user.Email = request.Email;
            user.Name = request.Name;
            user.PhoneNumber = request.PhoneNumber;
            dbContext.users.Add(user);
            dbContext.SaveChanges();
            ConfirmEmail confirmEmail = new ConfirmEmail();
            confirmEmail.UserId = user.Id;
            confirmEmail.RequiredDateTime = DateTime.Now;
            confirmEmail.ExpiredDateTime = DateTime.Now.AddMinutes(5);
            confirmEmail.ConfirmCode = otp.ToString();
            dbContext.confirmEmails.Add(confirmEmail);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Đăng kí tài khoản thành công !", converter.EntityToDTO(user));
        }
        #endregion
        #region   Xác thực tài khoản
        public string XacThucDangKy(string code)
        {
            var confirmEmail = dbContext.confirmEmails.FirstOrDefault(x => x.ConfirmCode.Equals(code));
            if (confirmEmail == null)
            {
                return "Mã xác nhận không đúng";
            }
            var nguoiDung = dbContext.users.FirstOrDefault(x => x.Id == confirmEmail.UserId);
            nguoiDung.UserStatusId = 2;
            confirmEmail.IsConfirm = true;
            dbContext.users.Update(nguoiDung);
            dbContext.confirmEmails.Update(confirmEmail);
            dbContext.SaveChanges();
            return "Xác thực tài khoản thành công !";
        }


        #endregion
        #region Đăng nhập
        public ResponseObject<DTO_Token> Login(Request_Login request)
        {
            if(string.IsNullOrWhiteSpace(request.Email)
                ||string.IsNullOrWhiteSpace(request.Password)
                ||!Email.IsValiEmail(request.Email)) 
            {
                return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest,"Giá trị nhập không hợp lệ !",null);
            }
            var user = dbContext.users.FirstOrDefault(x => x.Email == request.Email);
            if(user == null)
                return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Email hoặc Password không chính xác", null);
            if (BCryptNet.Verify(request.Password, user.Password))
            {
                return _responseObjectToken.ResponseSuccess("Đăng nhập thành công",GenerateAccessToken(user));
            }
            return _responseObjectToken.ResponseError(StatusCodes.Status400BadRequest, "Mất kết nối !",null);
        }


        #endregion
        #region Quên mật khẩu
        public string ForgotPass(Request_ForgotPassword request)
        {
            
            if (string.IsNullOrWhiteSpace(request.Email)
                || !Email.IsValiEmail(request.Email))
            {
                return "Email không hợp lệ !";
            }
            var user = dbContext.users.FirstOrDefault(x => x.Email == request.Email);
            if (user == null)
                return "Email chưa tồn tại !";
            EmailTo emailTo = new EmailTo();
            emailTo.Mail = request.Email;
            emailTo.Subject = "QUÊN MẬT KHẨU";
            Random random = new Random();
            int otp = random.Next(100000, 1000000);
            emailTo.Content = "Mã otp đặt lại mật khẩu của bạn là:" + otp + "\n Mã xác nhận sẽ hết hạn sau 5 phút nữa !";
            SendEmail(emailTo);
            var confirnEmail = dbContext.confirmEmails.FirstOrDefault(x => x.UserId == user.Id);
            confirnEmail.ConfirmCode = otp.ToString();
            confirnEmail.IsConfirm = false;
            confirnEmail.ExpiredDateTime = DateTime.Now.AddMinutes(5);
            confirnEmail.RequiredDateTime= DateTime.Now;
            dbContext.confirmEmails.Update(confirnEmail);
            dbContext.SaveChanges();
            return "Kiểm tra email để lấy mã đặt lại mật khẩu !";

        }



        #endregion
        #region Tạo mật khẩu mới
        public ResponseObject<DTO_DangKi> CreateNewPassWord(Request_CreateNewPassword request)
        {

            if (string.IsNullOrWhiteSpace(request.Code))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng nhập mã xác nhận !", null);
            if (string.IsNullOrWhiteSpace(request.NewPassword))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng nhập mật khẩu đặt lại !", null);
            Regex regex = new Regex("[!@#$%^&*()_+{}\\[\\]:;<>,.?/~`]");
            if (request.NewPassword.Length < 8 || request.NewPassword.Length > 20 || !regex.IsMatch(request.NewPassword))
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Password phải từ 8-20 kí tự và chứa ít nhất 1 kí tự đặc biệt !", null);
            var confirnEmail = dbContext.confirmEmails.FirstOrDefault(x => x.ConfirmCode.Equals(request.Code));
            if (confirnEmail == null)
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận chưa đúng !", null);
            if (confirnEmail.ExpiredDateTime < DateTime.UtcNow)
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận đã hết hạn !", null);
            var user = dbContext.users.Find(confirnEmail.UserId);
            user.Password=BCryptNet.HashPassword(request.NewPassword);
            confirnEmail.IsConfirm = true;
            dbContext.confirmEmails.Update(confirnEmail);
            dbContext.users.Update(user);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Tạo mật khẩu mới thành công !", converter.EntityToDTO(user));

        }


        #endregion
        #region Đổi mật khẩu
        public string ChangePassword(Request_ChangePassword request, int userId)
        {
            var user = dbContext.users.Find(userId);
            if (!BCryptNet.Verify(request.PassOld, user.Password))
            {
                return "Mật khẩu không chính xác";
            }
            if (PassWord.CheckPassWord(request.PassNew) != request.PassNew)
                return PassWord.CheckPassWord(request.PassNew);
            user.Password = BCryptNet.HashPassword(request.PassNew);
            dbContext.users.Update(user) ;
            dbContext.SaveChanges();
            return "Đổi mật khẩu thành công !";
        }
        #endregion

        public IQueryable<DTO_DangKi> Hienthitimkiem(InputUser input, int pageSize, int pageNumber)
        {
            var currentUser = _httpContextAccessor.HttpContext.User;

            if (!currentUser.Identity.IsAuthenticated)
            {
                throw new ArgumentNullException("Tai khoan chua duoc xac thuc");
            }
            if (!currentUser.IsInRole("Admin"))
            {
                throw new ArgumentNullException("Nguoi dung khong co quyen dung chuc nang nay");
            }
            IQueryable<User> check = dbContext.users.AsQueryable();

            if (input.Id.HasValue)
            {
                check = check.Where(x=>x.Id==input.Id);
            }
            if (!string.IsNullOrWhiteSpace(input.Email))
            {
                check = check.Where(x => x.Email.ToLower().Contains(input.Email.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                check = check.Where(x => x.Name.ToLower().Contains(input.Name.ToLower()));
            }

            var result = check.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(x => converter.EntityToDTO(x));
            return result;
        }

        





    }
}

