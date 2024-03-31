using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Banner;
using Cinema.Payloads.Response;
using System.Security;

namespace Cinema.Services.Interfaces
{
    public interface IService_Banner
    {
        public Task<ResponseObject<DTO_Banner>> CreateBanner(Request_CreateBanner request);
        public Task<ResponseObject<DTO_Banner>> UpdateBanner(Request_UpdateBanner request);
        public string DeleteBanner(int Id);
        public IQueryable<DTO_Banner > GetBannerList(int pageSize,int pageNumber);
    }
}
