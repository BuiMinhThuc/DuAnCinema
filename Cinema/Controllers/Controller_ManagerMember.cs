using Cinema.Payloads.Requests.InputRequests;
using Cinema.Payloads.Requests.Request_User;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_ManagerMember : ControllerBase
    {
        private readonly IManagerMember service;

        public Controller_ManagerMember(IManagerMember service)
        {
            this.service = service;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers(int? pageSize, int? pageNumber)
        {
            return Ok(service.GetAllMember(pageSize, pageNumber));
        }
        [HttpPut("UpdateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser(Request_UpdateUser request)
        {
            return Ok(service.UpdateMember(request));
        }
        [HttpPut("LockUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin")]
        public IActionResult LockUser(int id)
        {
            return Ok(service.LockUsser(id));
        }
    }
}
