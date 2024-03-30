using System.ComponentModel.DataAnnotations;

namespace Cinema.Payloads.Requests
{
    public class Request_ImageURL
    {
        [DataType(DataType.Upload)]
        public IFormFile Avatar { get; set; }
    }
}
