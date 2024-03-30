using Cinema.Entities;
using Cinema.Payloads.DTO;

namespace Cinema.Payloads.Converters
{
    public class Converter_Schedules
    {
        public DTO_Schedules EntityToDTO(Schedule schedule)
            => new DTO_Schedules
            {
                Id = schedule.Id,
                Price = schedule.Price,
                StartAt = schedule.StartAt,
                EndAt = schedule.EndAt,
                Code = schedule.Code,
                MovieId = schedule.MovieId,
                Name = schedule.Name,
                RoomId = schedule.RoomId,   
                IsActive = schedule.IsActive,
            };
    }
}
