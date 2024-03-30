using Azure.Core;
using Cinema.DataContext;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO.Users_DTO;

using Cinema.Payloads.Requests.Request_User;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;
using System.Linq;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Cinema.Services.Implements
{
    public class Service_ManagerMember : IManagerMember
    {
        private readonly AppDbContext dbContext;
        private readonly Converter_DangKi converter;
        private readonly ResponseObject<DTO_DangKi> responseObject;

        public Service_ManagerMember(AppDbContext dbContext, Converter_DangKi converter, ResponseObject<DTO_DangKi> responseObject)
        {
            this.dbContext = dbContext;
            this.converter = converter;
            this.responseObject = responseObject;
        }

        public IQueryable<DTO_DangKi> GetAllMember(int? pagesize, int? pageNumber)
        {
            var query = dbContext.users.Where(x => x.RoleId == 4).AsQueryable();
            if(pageNumber.HasValue && pagesize.HasValue)
            {
                query = query.Skip((pageNumber.Value-1)*pagesize.Value).Take(pagesize.Value);
            }
            return query.Select(x=>converter.EntityToDTO(x));
        }

        public string LockUsser(int id)
        {
            var user = dbContext.users.Find(id);
            if (user is null) return "User không tồn tại !";
            user.IsActive = false;
            dbContext.Update(user);
            dbContext.SaveChanges();
            return $"Khóa tài khoản có id :{id} thành công ";
        }

        public ResponseObject<DTO_DangKi> UpdateMember(Request_UpdateUser request)
        {
            var user = dbContext.users.Find(request.Id);
            if (user is null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "User không tồn tại !", null);
            user.Point = request.Point; 
            user.Email=request.Email;
            user.Password= BCryptNet.HashPassword(request.Password);
            user.IsActive = request.IsActive;
            user.Name = request.Name;
            user.Username=request.Username;
            user.PhoneNumber =request.PhoneNumber;
            user.RankCustomerId= request.RankCustomerId;
            user.UserStatusId= request.UserStatusId;
            user.RoleId= request.RoleId;
            dbContext.users.Update(user);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess( "Cập nhật thành công !", converter.EntityToDTO(user));
        }
    }
}
