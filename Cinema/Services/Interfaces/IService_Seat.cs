using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Seat;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IService_Seat
    {
        public ResponseObject<DTO_Seat> CreatSeat(Request_CreatSeat request);
        public ResponseObject<DTO_Seat> UpdateSeat(Request_UpdateSeat request);
        public string XoaSeat(string Id);

    }
}
