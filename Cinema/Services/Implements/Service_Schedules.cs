using Azure.Core;
using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Schedules;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;

namespace Cinema.Services.Implements
{
    public class Service_Schedules : IService_Schedules
    {
        private readonly ResponseObject<DTO_Schedules> responseObject;
        private readonly Converter_Schedules converter;
        private readonly AppDbContext dbContext;

        public Service_Schedules(AppDbContext appDbContext, ResponseObject<DTO_Schedules> responseObject, Converter_Schedules converter)
        {
            this.responseObject = responseObject;
            this.converter = converter;
            dbContext = appDbContext;
        }
        public static bool IsNewMovieScheduleValid(DateTime newStartTime, DateTime newEndTime, List<Schedule> existingSchedules)
        { 
            foreach (var schedule in existingSchedules)
            {
                if ((newStartTime >= schedule.StartAt && newStartTime <= schedule.EndAt) ||
                    (newEndTime >= schedule.StartAt && newEndTime <= schedule.EndAt) ||
                    (newStartTime <= schedule.StartAt && newEndTime >= schedule.EndAt))
                {   
                    return false;
                }
            }
            return true;
        }
        public ResponseObject<DTO_Schedules> CreateSchedules(Request_CreateSchedule request)
        {
            var schedules = new Schedule();
            schedules.Price = request.Price;
            
            if(!IsNewMovieScheduleValid(request.StartAt,request.EndAt,dbContext.schedules.ToList()))
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest,"Hiện tại đag có phim chiếu ở room này rồi !",null);
            }
            if( request.StartAt>request.EndAt)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Giờ bắt đầu phải nhỏ hơn giờ kết thúc !", null);
            }
            schedules.StartAt= request.StartAt;
            schedules.EndAt= request.EndAt;
            schedules.Code = request.Code;
            var movie = dbContext.movies.Find(request.MovieId);
            if (movie == null) { return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Không tìm thấy movie ID !", null); }
            schedules.MovieId= movie.Id;
            schedules.Name = request.Name;
            var room = dbContext.rooms.Find(request.RoomId);
            if (room == null) { return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Không tìm thấy room ID !", null); }
            schedules.RoomId= request.RoomId;
            dbContext.schedules.Add(schedules);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Thêm Schedules thành công !",converter.EntityToDTO(schedules));
        }

        public string DeleteSchedules(int id)
        {

            var schedules = dbContext.schedules.Find(id);
            if (schedules == null)
            {
                return "Không tồn tại schedules này !";
            }
            schedules.IsActive = false;
            dbContext.Update(schedules);
            dbContext.SaveChanges();
            return $"Xóa schedule {id} thành công !";
        }

        public IQueryable<DTO_Schedules> GetAllSchedules(int pageSize, int pageNumber)
        {
            var query = dbContext.schedules
                .Skip((pageNumber-1)*pageSize)
                .Take(pageSize)
                .Select(x=>converter.EntityToDTO(x))
                .AsQueryable();
            return query;
        }

        public ResponseObject<DTO_Schedules> UpdateSchedules(Request_UpdateSchedules request)
        {
            var schedules = dbContext.schedules.Find(request.Id);
            if (schedules == null) { return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Không tìm thấy schedules ID !", null); }
            schedules.Price = request.Price;

            if (!IsNewMovieScheduleValid(request.StartAt, request.EndAt, dbContext.schedules.Where(x=>x.Id!=request.Id).ToList()))
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Hiện tại đag có phim chiếu ở room này rồi !", null);
            }
            if (request.StartAt > request.EndAt)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Giờ bắt đầu phải nhỏ hơn giờ kết thúc !", null);
            }
            schedules.StartAt = request.StartAt;
            schedules.EndAt = request.EndAt;
            schedules.Code = request.Code;
            var movie = dbContext.movies.Find(request.MovieId);
            if (movie == null) { return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Không tìm thấy movie ID !", null); }
            schedules.MovieId = movie.Id;
            schedules.Name = request.Name;
            var room = dbContext.rooms.Find(request.RoomId);
            if (room == null) { return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Không tìm thấy room ID !", null); }
            schedules.RoomId = request.RoomId;
            schedules.IsActive = request.IsActive;
            dbContext.schedules.Update(schedules);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Sửa Schedules thành công !", converter.EntityToDTO(schedules));
        }
    }
}
