using System.ComponentModel.DataAnnotations;

namespace Cinema.Payloads.Requests.Request_Movie
{
    public class Request_inputMovie
    {
     
        public int? CinemaId {  get; set; }
     
        public int? RoomId {  get; set; }
     
        public int? SeatStatusId {  get; set; }
    }
}
