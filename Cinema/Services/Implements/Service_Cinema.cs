using Cinema.Entities;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;
using Cinema.Entities;
using Cinema.Payloads.Converters;
using Microsoft.IdentityModel.Tokens;
using Azure.Core;
using Cinema.Payloads.Requests.Request_Cinema;
using Cinema.DataContext;
using Microsoft.EntityFrameworkCore;
using Cinema.Payloads.DTO.DTO_Cinema;

namespace Cinema.Services.Implements
{
    public class Service_Cinema : IService_Cinema
    {
        private readonly ResponseObject<DTO_Cinema> response;
        private readonly Cinema.Payloads.Converters.Converter_Cinema converter;
        private readonly AppDbContext dbContext;
        

        public Service_Cinema(AppDbContext appDbContext, ResponseObject<DTO_Cinema> response, Converter_Cinema converter)
        {
            response = response;
            this.converter = converter;
            dbContext = appDbContext;
        }

        public ResponseObject<DTO_Cinema> CreatCNM(Request_CreateCinema request)
        {
            if (string.IsNullOrEmpty(request.Address) || string.IsNullOrEmpty(request.NameOfCinema)||string.IsNullOrEmpty(request.Description)||string.IsNullOrEmpty(request.Code)) 
            {
                   return response.ResponseError(StatusCodes.Status400BadRequest,"Vui lòng nhập đủ thông tin !",null);
            }
            var newCinema = new Entities.Cinema();
            newCinema.Address = request.Address??"";
            newCinema.NameOfCinema = request.NameOfCinema??"";
            newCinema.Description = request.Description ?? "";    
            newCinema.Code = request.Code ?? "";
            dbContext.cinemas.Add(newCinema);
            dbContext.SaveChanges();
            return response.ResponseSuccess("Thêm cinema thành công !",converter.EntityDTO(newCinema));
        }

        public string DeleteCinema(string id)
        {
            if (string.IsNullOrEmpty(id))
                return "Vui lòng nhập id Cinema cần xóa !";
            var newCinema = dbContext.cinemas.Find(int.Parse(id));
            if (newCinema == null)
                return "Không tồn tại Cinema này !";
            newCinema.IsActive = false;
            dbContext.cinemas.Update(newCinema);
            dbContext.SaveChanges();
            return "Xóa thành công !";
        }

        public IQueryable<DTO_ThongKeDoanhSo_Cinema> ThongKeDoanhSo_Cinema(Request_InputThongKeCinema? request)
        {

            var query = dbContext.bills.Include(x => x.BillTickets)
                .ThenInclude(x => x.Ticket)
                .ThenInclude(x => x.Schedule)
                .ThenInclude(x => x.Room)
                .ThenInclude(x => x.Cinema)
                .AsNoTracking()
                .Where(x => x.IsActive == true)
                .SelectMany(x => x.BillTickets)
                .GroupBy(x => x.Ticket.Schedule.Room.Cinema).AsQueryable();


            if (request.Star > request.End)
            {
                return null;
            }

            if (request.CinemaId.HasValue)
            {
                query = query.Where(x=>x.Key.Id==request.CinemaId);
                
            }
            if (request.Star.HasValue)
            {
                query = query.Where(x=>x.Any(x=>x.Bill.CreateTime>=request.Star));

            }
            if (request.End.HasValue)
            {
                query = query.Where(x => x.Any(x => x.Bill.CreateTime <= request.End));

            }
            

            return query.Select(x=> new DTO_ThongKeDoanhSo_Cinema
            {
                Id=x.Key.Id,
                NameOfCinema=x.Key.NameOfCinema,
                Money=x.Sum(x=>x.Bill.TotalMoney)
            });
        }

        public ResponseObject<DTO_Cinema> UpdateCinema(Request_UpdateCinema request)
        {
            if (string.IsNullOrEmpty(request.Id.ToString()) || string.IsNullOrEmpty(request.Address) || string.IsNullOrEmpty(request.NameOfCinema) || string.IsNullOrEmpty(request.Description) || string.IsNullOrEmpty(request.Code))
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng nhập đủ thông tin !", null);
            }
            var newCinema = dbContext.cinemas.Find(request.Id);
            if (newCinema == null)
                return response.ResponseError(StatusCodes.Status400BadRequest, "Không tồn tại Cinema này !", converter.EntityDTO(newCinema));
            newCinema.Address = request.Address ?? "";
            newCinema.NameOfCinema = request.NameOfCinema ?? "";
            newCinema.Description = request.Description ?? "";
            newCinema.Code = request.Code ?? "";
            dbContext.cinemas.Update(newCinema);
            dbContext.SaveChanges();
            return response.ResponseSuccess("Sửa cinema thành công !", converter.EntityDTO(newCinema));
        }
    }
}
