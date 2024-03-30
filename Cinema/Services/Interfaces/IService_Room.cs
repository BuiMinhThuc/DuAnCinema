using Azure;
using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Room;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IService_Room
    {
        public ResponseObject<DTO_Room> CreateRoom(Request_CreateRoom request);
        public ResponseObject<DTO_Room> UpdateRoom(Request_UpdateRoom request);
        public string DeleteRoom(string roomId);
    }
}
