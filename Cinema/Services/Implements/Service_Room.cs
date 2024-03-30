using Azure;
using Azure.Core;
using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Room;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;

namespace Cinema.Services.Implements
{
    public class Service_Room : IService_Room
    {
        private readonly ResponseObject<DTO_Room> response;
        private readonly Converter_Room converter;
        private readonly AppDbContext dbContext;

        public Service_Room(AppDbContext appDbContext, ResponseObject<DTO_Room> response, Converter_Room converter)
        {
            this.response = response;
            this.converter = converter;
            dbContext = appDbContext;
        }

        public ResponseObject<DTO_Room> CreateRoom(Request_CreateRoom request)
        {
            if (string.IsNullOrEmpty(request.Name)
                || string.IsNullOrEmpty(request.Capacity.ToString())
                ||string.IsNullOrEmpty(request.Code.ToString())
                ||string.IsNullOrEmpty(request.Description)
                || string.IsNullOrEmpty(request.CinemaId.ToString())
                || string.IsNullOrEmpty(request.Type.ToString()))
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin !", null);
            }
            var room = new Room();
            room.Name = request.Name;
            room.Capacity = request.Capacity;
            room.Description = request.Description;
            room.Code= request.Code;
            var Cinema = dbContext.cinemas.Find(request.CinemaId);
            if (Cinema == null)
                return response.ResponseError(StatusCodes.Status400BadRequest, "Cinema không tồn tại !", null);
            room.CinemaId = request.CinemaId;
            room.Type = request.Type;
           dbContext.rooms.Add(room);
            dbContext.SaveChanges();
            return response.ResponseSuccess("Thêm thành công !",converter.EntityDTO(room));
        }

        public string DeleteRoom(string roomId)
        {
            if (string.IsNullOrEmpty(roomId))
                return "Vui lòng nhập Id room !";
            var room = dbContext.rooms.Find(int.Parse(roomId));
            room.IsActive = false;
            dbContext.rooms.Update(room);
            dbContext.SaveChanges();
            return "Xóa thành công !";
        }

        public ResponseObject<DTO_Room> UpdateRoom(Request_UpdateRoom request)
        {
            if (string.IsNullOrEmpty(request.Id.ToString())
               || string.IsNullOrEmpty(request.Capacity.ToString())
               || string.IsNullOrEmpty(request.Name)
               || string.IsNullOrEmpty(request.Code.ToString())
               || string.IsNullOrEmpty(request.Description)
               || string.IsNullOrEmpty(request.CinemaId.ToString())
               || string.IsNullOrEmpty(request.Type.ToString()))
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin !", null);
            }
            var room = dbContext.rooms.Find(request.Id);
            if(room == null) return response.ResponseError(StatusCodes.Status400BadRequest, "Room không tồn tại !", null);
            room.Name = request.Name;
            room.Capacity = request.Capacity;
            room.Description = request.Description;
            room.Code = request.Code;
            var Cinema = dbContext.cinemas.Find(request.CinemaId);
            if (Cinema == null)
                return response.ResponseError(StatusCodes.Status400BadRequest, "Cinema không tồn tại !", null);
            room.CinemaId = request.CinemaId;
            room.Type = request.Type;
            dbContext.rooms.Update(room);
            dbContext.SaveChanges();
            return response.ResponseSuccess("Sửa thành công !", converter.EntityDTO(room));
        }
    }
}
