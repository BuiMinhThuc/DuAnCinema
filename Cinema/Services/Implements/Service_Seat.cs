using Azure;
using Azure.Core;
using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Seat;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;

namespace Cinema.Services.Implements
{
    public class Service_Seat: IService_Seat
    {
        private readonly ResponseObject<DTO_Seat> responseObject;
        private readonly Converter_Seat converter;
        private readonly AppDbContext dbContext;

        public Service_Seat(AppDbContext appDbContext, ResponseObject<DTO_Seat> responseObject, Converter_Seat converter)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter = converter;
        }

        public ResponseObject<DTO_Seat> CreatSeat(Request_CreatSeat request)
        {
            if(string.IsNullOrEmpty(request.RoomId.ToString())
                ||string.IsNullOrEmpty(request.Number.ToString())
                || string.IsNullOrEmpty(request.SeatTypeId.ToString())
                || string.IsNullOrEmpty(request.Line.ToString())) {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin !", null);
            }
            var seat = new Seat();
            var room = dbContext.rooms.Find(request.RoomId);
            if (room is null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Room không tồn tại !", null);
            seat.RoomId = request.RoomId;
            seat.Number = request.Number;
            seat.Line = request.Line;
            seat.SeatStatusId = 1;
            var seatType = dbContext.rooms.Find(request.SeatTypeId);
            if (seatType is null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Seat Type không tồn tại !", null);
            seat.SeatTypeId = request.SeatTypeId;
            dbContext.seats.Add(seat);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Thêm thành công !", converter.EntityToDTO(seat));
        }

        public ResponseObject<DTO_Seat> UpdateSeat(Request_UpdateSeat request)
        {

            if (string.IsNullOrEmpty(request.RoomId.ToString())
                ||string.IsNullOrEmpty(request.Id.ToString())
                || string.IsNullOrEmpty(request.SeatTypeId.ToString())
                || string.IsNullOrEmpty(request.SeatStatusId.ToString())
                || string.IsNullOrEmpty(request.Number.ToString())
                || string.IsNullOrEmpty(request.Line.ToString()))
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin !", null);
            }
            var seat = dbContext.seats.Find(request.Id);
            if (seat is null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Seat không tồn tại !", null);
            var room = dbContext.rooms.Find(request.RoomId);
            Console.WriteLine(request.SeatStatusId);
            if (room is null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Room không tồn tại !", null);
            SeatStatus seatStatusc = dbContext.seatsStatus.FirstOrDefault(x=>x.Id==request.SeatStatusId);
            if (seatStatusc is null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "SeatStatus không tồn tại !", null);
            seat.SeatStatusId = request.SeatStatusId;
            seat.RoomId = request.RoomId;
            seat.Number = request.Number;
            seat.Line = request.Line;
         
            var seatType = dbContext.rooms.Find(request.SeatTypeId);
            if (seatType is null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Seat Type không tồn tại !", null);
            seat.SeatTypeId = request.SeatTypeId;
            dbContext.seats.Update(seat);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Sửa thành công !", converter.EntityToDTO(seat));
        }

        public string XoaSeat(string Id)
        {
            if (string.IsNullOrEmpty(Id.ToString()))
                return "Vui lòng điền đầy đủ thông tin !";
            var seat = dbContext.seats.Find(int.Parse(Id));
            if (seat is null) return "Seat không tồn tại !";
            seat.IsActive = false;
            dbContext.seats.Update(seat);
            dbContext.SaveChanges();
            return "Xóa seat thành công !";


        }
    }
}
