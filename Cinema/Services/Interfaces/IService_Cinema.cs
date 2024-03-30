using Cinema.Payloads.DTO.DTO_Cinema;
using Cinema.Payloads.Requests.Request_Cinema;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IService_Cinema
    {
        public ResponseObject<DTO_Cinema> CreatCNM(Request_CreateCinema request);
        public ResponseObject<DTO_Cinema> UpdateCinema(Request_UpdateCinema request);
        public string DeleteCinema(string id);
        public IQueryable<DTO_ThongKeDoanhSo_Cinema> ThongKeDoanhSo_Cinema(Request_InputThongKeCinema? request);
    }
}
