using Cinema.Entities;
using Cinema.Payloads.DTO;

namespace Cinema.Payloads.Converters
{
    public class Converter_Banner
    {
        public DTO_Banner EntityToDTO(Banner banner)
            => new DTO_Banner
            {
                ImageUrl=banner.ImageUrl,
                Title = banner.Title,
            };
    }
}
