using Cinema.Entities;
using Cinema.Handle;
using Cinema.Payloads.DTO.Users_DTO;
using Cinema.Payloads.Requests.InputRequests;
using Cinema.Payloads.Requests.Users_RQ;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IService_Authen
    {
        public ResponseObject<DTO_DangKi> DangKi(Request_DangKi request);
        string GenerateRefreshToken();
        DTO_Token GenerateAccessToken(User user);
        ResponseObject<DTO_Token> RenewAccessToken(DTO_Token request);
        string XacThucDangKy(string code);
        public ResponseObject<DTO_Token> Login(Request_Login request);
        public string ForgotPass(Request_ForgotPassword request);
        public ResponseObject<DTO_DangKi> CreateNewPassWord(Request_CreateNewPassword request);
        public string ChangePassword(Request_ChangePassword request, int userId);
        public IQueryable<DTO_DangKi> Hienthitimkiem(InputUser input, int pageSize, int pageNumber);
        string SendEmail(EmailTo emailTo);
    }
}
