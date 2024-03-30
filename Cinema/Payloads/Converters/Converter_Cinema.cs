using Cinema.Payloads.DTO.DTO_Cinema;

namespace Cinema.Payloads.Converters
{
    public class Converter_Cinema
    {
        public DTO_Cinema EntityDTO(Cinema.Entities.Cinema cinema)
            => new DTO_Cinema
            {
                Id = cinema.Id,
                Address = cinema.Address,
                Description = cinema.Description,
                Code = cinema.Code,
                NameOfCinema= cinema.NameOfCinema,
                IsAtive = cinema.IsActive,
            };
        public DTO_ThongKeDoanhSo_Cinema EntityDTOs(Cinema.Entities.Cinema cinema,double TongTien)
           => new DTO_ThongKeDoanhSo_Cinema
           {
               Id = cinema.Id,
               Money=TongTien,
               
               NameOfCinema = cinema.NameOfCinema,
             
           };
    }
}
