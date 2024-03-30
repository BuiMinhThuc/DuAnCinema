using Cinema.Payloads.DTO.DTO_Food;
using Cinema.Payloads.Requests.Request_Food;
using Cinema.Payloads.Requests.Request_Room;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IService_Food
    {
        public  Task<ResponseObject<DTO_Food>> CreateFood(Request_CreateFood request);
        public Task<ResponseObject<DTO_Food>> UpdateFood(Request_UpdateFood request);
        public string DeleteFood(string foodId);
        public IQueryable<DTO_TopFood> TopFood();
    }
}
