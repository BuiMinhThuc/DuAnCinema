using Cinema.Entities;
using Cinema.Payloads.DTO.Users_DTO;
using Cinema.Payloads.Requests.InputRequests;
using Cinema.Payloads.Requests.Users_RQ;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Authen : ControllerBase
    {
        private readonly IService_Authen IDangki;
        private readonly IConfiguration _configuration;

        public Controller_Authen(IService_Authen iDangki, IConfiguration configuration)
        {
            
            IDangki = iDangki;
            _configuration = configuration;
        }

        [HttpPost("Đăng kí")]
        public IActionResult DangKi([FromBody]Request_DangKi request)
        =>Ok(IDangki.DangKi(request));
        [HttpPut("Xác thực tài khoản")]
        public IActionResult XacThucTaiKhoan(string request)
        {
            return Ok(IDangki.XacThucDangKy(request));
        }
        [HttpPost]
        [Route("Renew-token")]
        public IActionResult RenewToken(DTO_Token token)
        {
            var result = IDangki.RenewAccessToken(token);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }
        [HttpPost("Login")]
        public IActionResult Login(Request_Login request)
        {
            return Ok(IDangki.Login(request));
        }
        [HttpPut("Quên mật khẩu")]
        public IActionResult ForgotPass(Request_ForgotPassword request)
        {
            return Ok(IDangki.ForgotPass(request));
        }
        [HttpPut("Tạo mật khẩu mới")]
        public IActionResult CreateNewPassWord(Request_CreateNewPassword request)
        {
            return Ok(IDangki.CreateNewPassWord(request));
        }

        [HttpPut("Đổi mật khẩu")]
         [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
       // [ServiceFilter(typeof(CustomAuthorizeAttribute))]
        public IActionResult ChangePassword(Request_ChangePassword request)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }

            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            var check = IDangki.ChangePassword(request, id);
            if (check is null)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            return Ok(IDangki.ChangePassword(request, id));
        }

        [HttpGet("GetAllUsers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers([FromQuery]InputUser input,int pageSize = 10, int pageNumber = 1)
        {
            return Ok(IDangki.Hienthitimkiem(input,pageSize, pageNumber));
        }
    }
}
