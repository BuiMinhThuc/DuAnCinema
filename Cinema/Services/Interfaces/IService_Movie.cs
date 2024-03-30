using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Movie;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IService_Movie
    {
        public Task<ResponseObject<DTO_Movie>> CreateMovie(Request_CreateMovie request);
        public Task<ResponseObject<DTO_Movie>> UpdateMovie(Request_UpdateMovie request);
        public string DeleteMovie(string id);
        public IQueryable<DTO_Movie> HotMovieA_Z(int pageSize,int pageNumber);
        public IQueryable<DTO_Movie> ViewMovie_Cinema_Room_Seat(Request_inputMovie request,int pageSize,int pageNumber);

    }
}
