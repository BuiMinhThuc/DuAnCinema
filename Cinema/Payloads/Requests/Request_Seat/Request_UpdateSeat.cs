namespace Cinema.Payloads.Requests.Request_Seat
{
    public class Request_UpdateSeat
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int SeatStatusId { get; set; }
        public int SeatTypeId { get; set; }
        public string Line { get; set; }
        public int RoomId { get; set; }
    }
}
