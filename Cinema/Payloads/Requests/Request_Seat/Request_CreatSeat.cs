namespace Cinema.Payloads.Requests.Request_Seat
{
    public class Request_CreatSeat
    {
        public int Number { get; set; }
        public string Line { get; set; }
        public int RoomId { get; set; }
        public int SeatTypeId {  get; set; }
    }
}
