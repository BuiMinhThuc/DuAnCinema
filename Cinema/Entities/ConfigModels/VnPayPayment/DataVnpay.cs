using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCourseManagement_Models.ConfigModels.VnPayPayment
{
    public class DataVnpay
    {
        public string? TmnCode { get; set; }
        public string? HashSecret { get; set; }
        public string? ReturnUrl { get; set; }
        public string? BaseUrl { get; set; }
        public string? Command { get; set; }
        public string? CurrCode { get; set; }
        public string? Version { get; set; }
        public string? Locale { get; set; }
    }
}
