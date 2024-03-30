using Cinema.Entities;
using Cinema.Payloads.DTO;

namespace Cinema.Payloads.Converters
{
    public class Converter_Seat
    {
        public DTO_Seat EntityToDTO(Seat seat)
            => new DTO_Seat
               {
                    Id = seat.Id,
                    SeatStatusId = seat.SeatStatusId,
                    Line = seat.Line,
                    Number = seat.Number,
                    RoomId = seat.RoomId,
                    SeatTypeId = seat.SeatTypeId
               };
        
    }
}
