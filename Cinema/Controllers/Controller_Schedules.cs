using Cinema.Payloads.Requests.Request_Schedules;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_Schedules : ControllerBase
    {
        private readonly IService_Schedules service;

        public Controller_Schedules(IService_Schedules _service)
        {

            service = _service;
        }
        [HttpPost("Thêm Schedules")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult CreateSchedules(Request_CreateSchedule request)
        {
            return Ok(service.CreateSchedules(request));
        }
        [HttpPut("Sửa Schedules")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult UpdateSchedules(Request_UpdateSchedules request)
        {
            return Ok(service.UpdateSchedules(request));
        }
        [HttpPut("Xóa Schedules")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult DeleteSchedules(int id)
        {
            return Ok(service.DeleteSchedules(id));
        }
        [HttpGet("Hiển thị all Schedules")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult GetAllSchedules(int pageSize, int pageNumber)
        {
            return Ok(service.GetAllSchedules(pageSize, pageNumber));
        }

    }
}
