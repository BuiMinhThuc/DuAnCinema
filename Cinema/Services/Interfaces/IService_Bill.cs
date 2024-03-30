using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Bill;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IService_Bill
    {
        public  ResponseObject<DTO_Bill> TaoHoaDon(Request_CreateBill request,int UserId); 
    }
}
