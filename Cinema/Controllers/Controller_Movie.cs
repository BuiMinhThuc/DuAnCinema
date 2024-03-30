using Cinema.Payloads.Requests.Request_Food;
using Cinema.Payloads.Requests.Request_Movie;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Movie : ControllerBase
    {
        private readonly IService_Movie service;

        public Controller_Movie(IService_Movie _service)
        {

            service = _service;
        }
        [HttpPost("Thêm Movie")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ThemMovie(
           [FromForm] Request_CreateMovie request)
        => Ok(await service.CreateMovie(request));


        [HttpPut("Sửa Movie")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SuaMovie(Request_UpdateMovie request)
       => Ok(await service.UpdateMovie(request));


        [HttpPut("Xóa Movie")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult XoaFood([FromForm] string Id)
     => Ok(service.DeleteMovie(Id));

        [HttpGet("Lấy Movie nổi bật")]
        public IActionResult HotMovie(int pageSize, int pageNumber)
     => Ok(service.HotMovieA_Z(pageSize,pageNumber));
        
        [HttpGet("Lấy Movie theo cinema,room,seatStatus")]
        public IActionResult viewMovie([FromQuery]Request_inputMovie? request,int pageSize,int pageNumber)
     => Ok(service.ViewMovie_Cinema_Room_Seat(request,pageSize,pageNumber));

    }

}
