namespace Cinema.Payloads.Requests.Request_Banner
{
    public class Request_UpdateBanner
    {
        public int Id { get; set; }
        public IFormFile ImageUrl { get; set; }
        public string Title { get; set; }
    }
}
