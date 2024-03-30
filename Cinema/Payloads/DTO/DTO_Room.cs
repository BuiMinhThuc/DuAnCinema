namespace Cinema.Payloads.DTO
{
    public class DTO_Room
    {
        public int ID { get; set; }
        public int Capacity { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public int CinemaId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
