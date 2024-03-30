using Cinema.Entities;
using Cinema.Payloads.DTO;

namespace Cinema.Payloads.Converters
{
    public class Converter_Room
    {
        public DTO_Room EntityDTO(Room room)
        {
            return new DTO_Room
            {
                ID= room.Id,
                CinemaId = room.CinemaId,
                Capacity = room.Capacity,
                Description= room.Description,
                Type= room.Type,
                Code= room.Code,
                Name= room.Name
            };
        }
    }
}
