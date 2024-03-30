using Cinema.Payloads.Requests.Request_Bill;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Bill : ControllerBase
    {
        private readonly IService_Bill service;

        public Controller_Bill(IService_Bill service)
        {
            this.service = service;
        }
        [HttpPost("Thêm Bill")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CreateBill(Request_CreateBill request)
        {

            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return Ok("Vui lòng đăng nhập !");
            }
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok(service.TaoHoaDon(request,id));
        }
    }
}
