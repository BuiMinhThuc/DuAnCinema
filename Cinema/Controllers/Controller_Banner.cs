using Cinema.Payloads.Requests.Request_Banner;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Banner : ControllerBase
    {
        private readonly IService_Banner service;

        public Controller_Banner(IService_Banner service)
        {
            this.service = service;
        }

        [HttpPost("Create Banner")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin,Manager,Staff")]
        [Consumes(contentType: "multipart/form-data")]
        public async Task<IActionResult> CreateBanner([FromForm] Request_CreateBanner request)
        {
            return  Ok(await service.CreateBanner(request));
        }
        [HttpPut("Update Banner")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public async Task<IActionResult> UpdateBanner(Request_UpdateBanner request)
        {
            return Ok(await service.UpdateBanner(request));
        }
        [HttpDelete("Delete Banner")]

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public IActionResult DeleteBanner(int Id)
        {
            return Ok(service.DeleteBanner(Id));
        }
        [HttpGet("getAll Banner")]

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin,Manager,Staff")]
        public IActionResult GetAllBanner(int pageSize=5,int pageNumber=1)
        {
            return Ok(service.GetBannerList(pageSize,pageNumber));
        }
    }
}
