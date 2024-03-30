using Cinema.Payloads.DTO.Users_DTO;
using Cinema.Payloads.Requests.Request_User;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IManagerMember
    {
        public IQueryable<DTO_DangKi> GetAllMember(int? pagesize, int? pageNumber);

        public ResponseObject<DTO_DangKi> UpdateMember(Request_UpdateUser request);
        public string LockUsser(int id);
    }
}
