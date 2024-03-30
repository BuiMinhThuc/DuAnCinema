using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Payloads.DTO.Users_DTO;
using Cinema.Services.Implements;

namespace Cinema.Payloads.Converters
{
    public class Converter_DangKi
    {
        private readonly AppDbContext dbContext;
        public Converter_DangKi(AppDbContext appDbContext)
        {
            dbContext = appDbContext;
        }

        public DTO_DangKi EntityToDTO(User user)
        {
            return new DTO_DangKi
            {
                Id = user.Id,
                Point = user.Point,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                RankCustomerName = dbContext.rankCustomers.FirstOrDefault(x => x.Id == user.RankCustomerId)?.Name,
                UserStatusName = dbContext.userStatuses.FirstOrDefault(x => x.Id == user.UserStatusId)?.Name,
                IsActive = user.IsActive,
                RoleName = dbContext.roles.FirstOrDefault(x => x.Id == user.RoleId)?.RoleName
            };
        }
    }
}
 