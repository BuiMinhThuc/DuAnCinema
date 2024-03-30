using Cinema.Services.Implements;

namespace Cinema.Payloads.DTO.DTO_Cinema
{
    public class DTO_Cinema
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string NameOfCinema { get; set; }
        public bool? IsAtive { get; set; }
    }
}
