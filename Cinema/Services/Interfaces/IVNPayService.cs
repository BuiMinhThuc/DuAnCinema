using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseManagement_Business.Interfaces
{
    public interface IVNPayService
    {
        string TaoDuongDanThanhToan(int hoaDonId, HttpContext httpContext, int id);
        string VNPayReturn(IQueryCollection vnpayData);
    }
}
