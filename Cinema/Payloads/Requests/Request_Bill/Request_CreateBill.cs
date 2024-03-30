using Cinema.Entities;

namespace Cinema.Payloads.Requests.Request_Bill
{
    public class Request_CreateBill
    {
        public int MovieId {  get; set; }
        public int CinemaId {  get; set; }
        public int RoomId {  get; set; }
        public int ScheduleId {  get; set; }
        public List<Request_BillFood> Foods { get; set; }
        public List<Request_BillTicket> Tickets {  get; set; }
        public string TradingCode {  get; set; }
        public string Name {  get; set; }

    }
}
