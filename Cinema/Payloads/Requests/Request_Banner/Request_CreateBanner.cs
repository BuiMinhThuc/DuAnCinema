namespace Cinema.Payloads.Requests.Request_Banner
{
    public class Request_CreateBanner
    {
        public IFormFile ImageUrl { get; set; }
        public string Title { get; set; }
    }
}
