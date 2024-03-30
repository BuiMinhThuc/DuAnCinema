using API_JWT_C_.Handler.Image;
using Azure.Core;
using Cinema.Entities;
using Cinema.Payloads.Converters;
using Cinema.Payloads.Requests.Request_Food;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;
using Cinema.Handle;
using AutoMapper;
using Cinema.DataContext;
using Cinema.Payloads.DTO.DTO_Food;

namespace Cinema.Services.Implements
{
    public class Service_Food : IService_Food
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ResponseObject<DTO_Food> response;
        private readonly Converter_Food converter;
        private readonly AppDbContext dbContext;

        public Service_Food(IMapper mapper,
            IConfiguration configuration, AppDbContext appDbContext, ResponseObject<DTO_Food> response, Converter_Food converter)
        {
            _mapper = mapper;
            _configuration = configuration;
            response = response;
            converter = converter;
            dbContext = appDbContext;
        }

        public async Task<ResponseObject<DTO_Food>> CreateFood(Request_CreateFood request)
        {
            if(string.IsNullOrWhiteSpace(request.Price.ToString())
                ||string.IsNullOrWhiteSpace(request.Description)
                ||string.IsNullOrWhiteSpace(request.NameOfFood)
                ||string.IsNullOrWhiteSpace(request.Image.ToString())){
                return response.ResponseError(StatusCodes.Status400BadRequest,"Vui lòng nhập đủ thông tin !", null);
            }
            var Food = new Food();
            Food.NameOfFood = request.NameOfFood;
            Food.Price = request.Price;
            Food.Description = request.Description;
            int imageSize = 2 * 1024 * 768;
            if (request.Image != null)
            {
                if (!HandleImage.IsImage(request.Image, imageSize))
                {
                    return response.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.Image);
                    Food.Image = avatarFile == "" ? "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                }
            }
            dbContext.foods.AddAsync(Food);
            dbContext.SaveChangesAsync();
            return response.ResponseSuccess("Thêm food thành công !",converter.EntityToDTO(Food));

        }






        public string DeleteFood(string foodId)
        {

            if (string.IsNullOrWhiteSpace(foodId.ToString()))
            {
                return "Vui lòng nhập đủ thông tin !";
            }
            var food = dbContext.foods.Find(int.Parse(foodId));
            if (food is null) return "Food không tồn tại !";
           food.IsActive = false;
            dbContext.foods.Update(food);
            dbContext.SaveChanges();
            return "Xóa food thành công !";
        }

        public IQueryable<DTO_TopFood> TopFood()
        {
            var query = dbContext.billFoods.GroupBy(x => x.Food).Where(x=>x.Any(x=>x.Bill.CreateTime>=DateTime.Now.AddDays(-7))).AsQueryable();


            return query.Select(x => new DTO_TopFood
            {
                Id= x.Key.Id,
                Price=x.Key.Price,
                NameOfFood=x.Key.NameOfFood,
                quantity=x.Sum(x=>x.Quantity)
            });
        }

        public async Task<ResponseObject<DTO_Food>> UpdateFood(Request_UpdateFood request)
        {
            if (string.IsNullOrWhiteSpace(request.Price.ToString())
               || string.IsNullOrWhiteSpace(request.Description)
               || string.IsNullOrWhiteSpace(request.Id.ToString())
               || string.IsNullOrWhiteSpace(request.NameOfFood)
               || string.IsNullOrWhiteSpace(request.Image.ToString()))
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng nhập đủ thông tin !", null);
            }
            var Food = dbContext.foods.Find(request.Id);
            if (Food is null) return response.ResponseError(StatusCodes.Status400BadRequest, "Food không tồn tại !", null);
            Food.NameOfFood = request.NameOfFood;
            Food.Price = request.Price;
            Food.Description = request.Description;
           Food.Image = await HandleUploadImage.UpdateFile(Food.Image, request.Image);
            Food.IsActive = request.IsActive;
            dbContext.foods.Update(Food);
            await dbContext.SaveChangesAsync();
            return response.ResponseSuccess("Sửa food thành công !", converter.EntityToDTO(Food));
        }
    }
}
