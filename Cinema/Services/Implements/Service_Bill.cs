using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Bill;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using WebCourseManagement_Business.Implements;

namespace Cinema.Services.Implements
{
    public class Service_Bill : IService_Bill
    {
        private readonly ResponseObject<DTO_Bill> response;
        private readonly Converter_Bill converter;
        private readonly AppDbContext dbContext;
  
        public Service_Bill(AppDbContext appDbContext, ResponseObject<DTO_Bill> response, Converter_Bill converter)
        {
            this.response = response;
            this.converter = converter;
            this.dbContext = appDbContext;
        }

        public  ResponseObject<DTO_Bill> TaoHoaDon(Request_CreateBill request, int UserId)
        {
            var movie = dbContext.movies.Find(request.MovieId);
            if( movie == null) {return response.ResponseError(StatusCodes.Status400BadRequest, "Không tồn tại movie này !", null); }
            var cinema1 = dbContext.cinemas.Find(request.CinemaId);
            if (cinema1 == null) { return response.ResponseError(StatusCodes.Status400BadRequest, "Không tồn tại cinema này !", null); }
            var room = dbContext.rooms.Find(request.RoomId);
            if (room == null) {return response.ResponseError(StatusCodes.Status400BadRequest, "Không tồn tại room này !", null); }

            foreach (var Ticket in request.Tickets)
            {
                foreach (var billTicket in dbContext.billTickets)
                {

                    if (billTicket.TicketId == Ticket.Id)
                        return response.ResponseError(StatusCodes.Status400BadRequest, "Ghế đã được đặt rồi!", null);
                }
            }
            double tongPrice=0;
            foreach(var item in request.Foods)
            {
                tongPrice =tongPrice+ dbContext.foods.Find(item.Id).Price *item.QuantityFood;
            }
            foreach (var item in request.Tickets)
            {
                tongPrice = tongPrice + dbContext.tickets.Find(item.Id).PriceTicket;
            }
            double GiamGia = dbContext.promotions
                .Include(x=>x.RankCustomer)
                .ThenInclude(x=>x.Users.Where(x=>x.Id==UserId))
                .Select(x=>x.Percent).FirstOrDefault();
                
            var  bill =  new Bill
            {

                TotalMoney = tongPrice - (tongPrice * GiamGia/100),
                TradingCode=request.TradingCode,
                UserId= UserId,
                Name= request.Name,
                PromotionID= dbContext.promotions
                .Include(x => x.RankCustomer)
                .ThenInclude(x => x.Users.Where(x => x.Id == UserId))
                .Select(x => x.Id).FirstOrDefault(),     
            };
            dbContext.bills.Add(bill);
            dbContext.SaveChanges();
            foreach (var item in request.Foods)
            {
                var billfood = new BillFood
                {
                    Quantity = item.QuantityFood,
                    BillId = bill.Id,
                    FoodId = item.Id,
                };
                dbContext.billFoods.Add(billfood);
                dbContext.SaveChanges();

            }
            foreach (var item in request.Tickets)
            {
                var billticket = new BillTicket
                {
                    Quantity = 1,
                    BillId = bill.Id,
                    TicketId = item.Id,
                };
                dbContext.billTickets.Add(billticket);
                dbContext.SaveChanges();
            }
            return  response.ResponseSuccess("Tạo bill thành công, vui lòng thanh toán !", converter.EntityToDTO(bill));
        }
    }
}
