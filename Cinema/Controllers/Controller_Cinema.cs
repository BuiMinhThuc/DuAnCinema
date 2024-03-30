using Cinema.Payloads.Requests.Request_Cinema;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Cinema : ControllerBase
    {
        private readonly IService_Cinema cinemaService;
       
        public Controller_Cinema(IService_Cinema cinemaService)
        {
            this.cinemaService = cinemaService;
           
        }

        [HttpPost("Thêm cinema")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult ThemCinema(Request_CreateCinema request) 
        =>Ok(cinemaService.CreatCNM(request));
        [HttpPut("Sửa cinema")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateCinema(Request_UpdateCinema request)
        => Ok(cinemaService.UpdateCinema(request));

        [HttpPut("Xóa cinema")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCinema([FromBody]string id)
        => Ok(cinemaService.DeleteCinema(id));
        [HttpGet("Thống kê theo doanh số cinema")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult ThongKeCinema([FromQuery] Request_InputThongKeCinema request)
        => Ok(cinemaService.ThongKeDoanhSo_Cinema(request));
    }
}
