using System.ComponentModel.DataAnnotations;

namespace Cinema.Payloads.Requests.Request_Food
{
    public class Request_UpdateFood
    {
        public int Id { get; set; }

   
        public double Price { get; set; }
      
        public string? Description { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Image { get; set; }
        public string NameOfFood { get; set; }
        public bool? IsActive { get; set; }
    }
}
