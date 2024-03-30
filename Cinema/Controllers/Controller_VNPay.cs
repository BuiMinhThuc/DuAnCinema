using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCourseManagement_Business.Implements;
using WebCourseManagement_Business.Interfaces;

namespace Cinema.Controllers
{
    [Route("api/vnpay")]
    [ApiController]
    public class Controller_VNPay : ControllerBase
    {
        private readonly IVNPayService _vnpayService;

        public Controller_VNPay(IVNPayService vNPayService)
        {
            _vnpayService = vNPayService;
        }
        [HttpPost("TaoDuongDanThanhToan/{hoaDonId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult TaoDuongDanThanhToan([FromRoute] int hoaDonId)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);
            return Ok( _vnpayService.TaoDuongDanThanhToan(hoaDonId, HttpContext, id));
        }
        [HttpGet("Return")]
        public  IActionResult Return()
        {
            var vnpayData = HttpContext.Request.Query;

            return Ok( _vnpayService.VNPayReturn(vnpayData));
        }
    }
}
