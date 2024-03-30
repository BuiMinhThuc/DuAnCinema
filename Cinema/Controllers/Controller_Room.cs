using Cinema.Payloads.Requests.Request_Room;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Room : ControllerBase
    {
        private readonly IService_Room service_Room;

        public Controller_Room(IService_Room _service_Room)
        {
            service_Room = _service_Room;
        }
        [HttpPost("Thêm room")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult ThemRoom(Request_CreateRoom request)
        => Ok(service_Room.CreateRoom(request));
        [HttpPut("Sửa room")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult SuaRoom( Request_UpdateRoom request)
       => Ok(service_Room.UpdateRoom(request));
        [HttpPut("Xóa room")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult XoaRoom(string Id)
     => Ok(service_Room.DeleteRoom(Id));

    }
}
