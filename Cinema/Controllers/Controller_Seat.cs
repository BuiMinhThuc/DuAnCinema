using Cinema.Payloads.Requests.Request_Room;
using Cinema.Payloads.Requests.Request_Seat;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Seat : ControllerBase
    {
        private readonly IService_Seat service_Seat;

        public Controller_Seat(IService_Seat _service_Seat)
        {
            service_Seat = _service_Seat;
        }
        [HttpPost("Thêm seat")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult ThemSeat(Request_CreatSeat request)
        => Ok(service_Seat.CreatSeat(request));


        [HttpPut("Sửa seat")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult SuaSeat(Request_UpdateSeat request)
       => Ok(service_Seat.UpdateSeat(request));


        [HttpPut("Xóa seat")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult XoaSeat(string Id)
     => Ok(service_Seat.XoaSeat(Id));
    }
}
