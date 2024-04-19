using Cinema.DataContext;
using Cinema.Payloads.Requests.Request_GeneralSettings;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller_GeneralSetting : ControllerBase
    {
        private readonly IService_GeneralSettings serviceGeneralSettings;

        public Controller_GeneralSetting(AppDbContext dbContext, IService_GeneralSettings serviceGeneralSettings)
        {
           
            this.serviceGeneralSettings = serviceGeneralSettings;
        }

        [HttpPost("Thêm GeneralSettings")]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles ="Admin,Manager")]
        public async Task<IActionResult> Them(Request_Create_GeneralSettings request)
        {
            return Ok(await serviceGeneralSettings.CreateGeneralSettings(request));
        }
        [HttpPut("Sửa GeneralSettings")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Sua(Request_Update_GeneralSettings request)
        {
            return Ok( await serviceGeneralSettings.UpdateGeneralSettings(request));
        }




    }
}
