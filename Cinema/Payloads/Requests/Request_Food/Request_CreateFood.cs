using System.ComponentModel.DataAnnotations;

namespace Cinema.Payloads.Requests.Request_Food
{
    public class Request_CreateFood
    {
        [Range(0, 100)]
        public double Price { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string NameOfFood { get; set; }
    }
}
