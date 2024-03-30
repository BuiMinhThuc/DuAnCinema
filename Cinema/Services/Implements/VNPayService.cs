using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Handle;
using Cinema.Services.Implements;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCourseManagement_Business.Interfaces;
using WebCourseManagement_Models.ConfigModels.VnPayPayment;


namespace WebCourseManagement_Business.Implements
{
    public class VNPayService : IVNPayService
    {
       
        private readonly IConfiguration _configuration;
        private readonly IService_Authen _authService;
        private readonly AppDbContext dbContext;

        public VNPayService()
        {
        }

        public VNPayService( IConfiguration configuration, IService_Authen authService, AppDbContext dbContext)
        {
            _configuration = configuration;
            _authService = authService;
            this.dbContext = dbContext;
        }

        public string TaoDuongDanThanhToan(int hoaDonId, HttpContext httpContext, int id)
        {
            var hoaDon =  dbContext.bills.SingleOrDefault(x => x.Id == hoaDonId);
            var nguoiDung = dbContext.users.SingleOrDefault(x => x.Id == id);   
            if (nguoiDung.Id == hoaDon.UserId)
            {
                
                if (hoaDon.BillStatusId == 2)
                {
                    return "Hóa đơn đã được thanh toán trước đó";
                }
                if (hoaDon.BillStatusId == 1 && hoaDon.TotalMoney != 0 && hoaDon.TotalMoney != null)
                {
                    VnPayLibrary vnpay = new VnPayLibrary();

                    vnpay.AddRequestData("vnp_Version", "2.1.0");
                    vnpay.AddRequestData("vnp_Command", "pay");
                    vnpay.AddRequestData("vnp_TmnCode", "77E3BAVI");
                    vnpay.AddRequestData("vnp_Amount", (hoaDon.TotalMoney*100).ToString());
                    vnpay.AddRequestData("vnp_CreateDate", hoaDon.CreateTime?.ToString("yyyyMMddHHmmss"));
                    vnpay.AddRequestData("vnp_CurrCode", "VND");
                    vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(httpContext));
                    vnpay.AddRequestData("vnp_Locale", "vn");
                    vnpay.AddRequestData("vnp_OrderInfo", "Thanh toán hóa đơn: " + hoaDon.Id);
                    vnpay.AddRequestData("vnp_OrderType", "other");
                    vnpay.AddRequestData("vnp_ReturnUrl", "https://localhost:7262/api/vnpay/Return");
                    vnpay.AddRequestData("vnp_TxnRef", hoaDon.Id.ToString());
                    string ma = _configuration.GetSection("VnPay:HashSecret").Value;
                    string duongDanThanhToan = vnpay.CreateRequestUrl(_configuration.GetSection("VnPay:BaseUrl").Value, "SJXWEBGTZQFLBTAOGRPGWHKWOHUFPUYB");
                    return duongDanThanhToan;
                }

                return "Vui lòng kiểm tra lại hóa đơn";
            }
            return "Vui lòng kiểm tra lại thông tin người thanh toán";
        }

        public string VNPayReturn(IQueryCollection vnpayData)
        {
            string vnp_TmnCode = _configuration.GetSection("VnPay:TmnCode").Value;
            string vnp_HashSecret = _configuration.GetSection("VnPay:HashSecret").Value;

            var vnPayLibrary = new VnPayLibrary();
            foreach (var (key, value) in vnpayData)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnPayLibrary.AddResponseData(key, value);
                }
            }

            string hoaDonId = vnPayLibrary.GetResponseData("vnp_TxnRef");
            string vnp_ResponseCode = vnPayLibrary.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnPayLibrary.GetResponseData("vnp_TransactionStatus");
            string vnp_SecureHash = vnPayLibrary.GetResponseData("vnp_SecureHash");
            double vnp_Amount = Convert.ToDouble(vnPayLibrary.GetResponseData("vnp_Amount"));
            bool check = vnPayLibrary.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

            if (check)
            {
                if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                {
                    var hoaDon =  dbContext.bills.FirstOrDefault(x => x.Id == Convert.ToInt32(hoaDonId));

                    if (hoaDon == null)
                    {
                        return "Không tìm thấy hóa đơn";
                    }

                    hoaDon.BillStatusId = 2;
                    hoaDon.CreateTime = DateTime.Now;

                    var user = dbContext.users.FirstOrDefault(x => x.Id == hoaDon.UserId);
                    if (user != null)
                    {
                        string email = user.Email;
                        string mss = _authService.SendEmail(new EmailTo
                        {
                            Mail = email,
                            Subject = $"Thanh Toán đơn hàng: {hoaDon.Id}",
                            Content = "Bạn đã đặt vé thành công!"
                        });
                    }
                    dbContext.bills.Update(hoaDon);
                    dbContext.SaveChanges();
                    return "Giao dịch thành công đơn hàng " + hoaDon.Id;
                }
                else
                {
                    return $"Lỗi trong khi thực hiện giao dịch. Mã lỗi: {vnp_ResponseCode}";
                }
            }
            else
            {
                return "Có lỗi trong quá trình xử lý";
            }
        }
    }
}
