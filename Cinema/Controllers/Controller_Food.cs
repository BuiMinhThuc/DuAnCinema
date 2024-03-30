using Cinema.Payloads.Requests.Request_Food;
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
    public class Controller_Food : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IService_Food service_Food;

        public Controller_Food(IService_Food _service_Food, IConfiguration configuration)
        {
            _configuration = configuration;
            service_Food = _service_Food;
        }
        [HttpPost("Thêm Food")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Consumes(contentType: "multipart/form-data")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ThemFood(
           [FromForm] Request_CreateFood request)
        => Ok(await service_Food.CreateFood(request));


        [HttpPut("Sửa Food")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SuaFood(Request_UpdateFood request)
       => Ok(await service_Food.UpdateFood(request));


        [HttpPut("Xóa Food")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult XoaFood([FromForm]string Id)
     => Ok(service_Food.DeleteFood(Id));

        [HttpGet("Thống kê Food bán chạy")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult TopFood()
     => Ok(service_Food.TopFood());


    }
}
