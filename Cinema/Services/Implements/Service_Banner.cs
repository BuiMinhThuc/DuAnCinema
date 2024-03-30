using API_JWT_C_.Handler.Image;
using Azure;
using Azure.Core;
using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Banner;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;

namespace Cinema.Services.Implements
{
    public class Service_Banner:IService_Banner
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_Banner> responseObject;
        private readonly Converter_Banner converter;

        public Service_Banner(AppDbContext dbContext, ResponseObject<DTO_Banner> responseObject, Converter_Banner converter)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter = converter;
        }

        public Service_Banner()
        {
        }

        public async Task<ResponseObject<DTO_Banner>> CreateBanner(Request_CreateBanner request)
        {
            int imageSize = 2 * 1024 * 768;
            var banner = new Banner();
            
            if (request.ImageUrl != null)
            {
                if (!HandleImage.IsImage(request.ImageUrl, imageSize))
                {
                    return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile =await HandleUploadImage.Upfile(request.ImageUrl);
                    banner.ImageUrl = avatarFile == ""?  "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=": avatarFile;
                }
            }
            banner.Title= request.Title;
            dbContext.banners.Add(banner);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Thêm banner thành công !",converter.EntityToDTO(banner));   
        }

        public string DeleteBanner(int Id)
        {
            var banner = dbContext.banners.FirstOrDefault(x=>x.Id==Id);
            if (banner is null)
                return "Không tồn tại banner này !";
            dbContext.banners.Remove(banner);
            dbContext.SaveChanges();
            return " Xóa thành công !";
        }

        public async Task<ResponseObject<DTO_Banner>> UpdateBanner(Request_UpdateBanner request)
        {
            int imageSize = 2 * 1024 * 768;
            var banner = dbContext.banners.Find(request.Id);
            if (banner == null)
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Không tồn tại banner này !", null);
            if (request.ImageUrl != null)
            {
                if (!HandleImage.IsImage(request.ImageUrl, imageSize))
                {
                    return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.UpdateFile(banner.ImageUrl,request.ImageUrl);
                    banner.ImageUrl = avatarFile == "" ? "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                }
            }
            banner.Title = request.Title;
            dbContext.banners.Update(banner);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Sửa banner thành công !", converter.EntityToDTO(banner));
        }
    }
}
