using API_JWT_C_.Handler.Image;
using Azure;
using Azure.Core;
using Cinema.Controllers;
using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Movie;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;
using CloudinaryDotNet.Core;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Cinema.Services.Implements
{
    public class Service_Movie : IService_Movie
    {
        private readonly ResponseObject<DTO_Movie> responseObject;
        private readonly Converter_Movie converterMovie;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext dbContext;
        public Service_Movie(IConfiguration configuration, AppDbContext appDbContext, ResponseObject<DTO_Movie> responseObject, Converter_Movie converter)
        {
            this.responseObject = responseObject;
            converterMovie = converter;
            _configuration = configuration;
            dbContext = appDbContext;

        }

        public async Task<ResponseObject<DTO_Movie>> CreateMovie(Request_CreateMovie request)
        {
            var movie = new Movie();
            movie.MovieDuration = request.MovieDuration;
            movie.EndTime = movie.EndTime;
            movie.PremiereDate = request.PremiereDate;
            movie.Description = request.Description;
            movie.Director = request.Director;
            int imageSize = 2 * 1024 * 768;
            if (request.Image != null)
            {
                if (!HandleImage.IsImage(request.Image, imageSize))
                {
                    return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.Image);
                    movie.Image = avatarFile == "" ? "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                }
            }
            if (request.HeroImage != null)
            {
                if (!HandleImage.IsImage(request.HeroImage, imageSize))
                {
                    return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.Upfile(request.HeroImage);
                    movie.HeroImage = avatarFile == "" ? "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                }
            }
            movie.Language = request.Language;
            var movietype = dbContext.movieTypes.Find(int.Parse(request.MovieTypeId.ToString()));
            if (movietype == null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "MovieTypeId không hợp lệ ! ", null);
            movie.MovieTypeId = request.MovieTypeId;
            movie.Name = request.Name;
            var rate = dbContext.movieTypes.Find(int.Parse(request.RateId.ToString()));
            if (rate == null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "RateId không hợp lệ ! ", null);
            movie.RateId = request.RateId;
            movie.Trailer = request.Trailer;
            await dbContext.movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseSuccess("Thêm Movie thành công !", converterMovie.EntityToDTO(movie));
        }

        public string DeleteMovie(string id)
        {
            var movie = dbContext.movies.Find(int.Parse(id.ToString()));
            if (movie == null) return "ID không tồn tại !";
            movie.IsActive = false;
            dbContext.movies.Update(movie);
            dbContext.SaveChanges();
            return "Xóa Movie thành công !";
        }

        public IQueryable<DTO_Movie> HotMovieA_Z(int pageSize,int pageNumber)
        {
            var query = dbContext.movies
                .Where(x => x.IsActive == true)
                .Include(x => x.Schedules)
                .ThenInclude(y => y.Tickets)
                .ThenInclude(z => z.BillTickets.OrderByDescending(z => z.Quantity)).AsQueryable();
            query=query.Skip((pageNumber-1)*pageSize).Take(pageSize);
            return query.Select(x => converterMovie.EntityToDTO(x));


        }

        public async Task<ResponseObject<DTO_Movie>> UpdateMovie(Request_UpdateMovie request)
        {
            var movie = dbContext.movies.Find(int.Parse(request.Id.ToString()));
            if (movie == null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "ID không tồn tại !", null);
            movie.MovieDuration = request.MovieDuration;
            movie.EndTime = movie.EndTime;
            movie.PremiereDate = request.PremiereDate;
            movie.Description = request.Description;
            movie.Director = request.Director;
            int imageSize = 2 * 1024 * 768;
            if (request.Image != null)
            {
                if (!HandleImage.IsImage(request.Image, imageSize))
                {
                    return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.UpdateFile(movie.Image, request.Image);
                    movie.Image = avatarFile == "" ? "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                }
            }
            if (request.HeroImage != null)
            {
                if (!HandleImage.IsImage(request.HeroImage, imageSize))
                {
                    return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Ảnh không hợp lệ", null);
                }
                else
                {
                    var avatarFile = await HandleUploadImage.UpdateFile(movie.HeroImage, request.HeroImage);
                    movie.HeroImage = avatarFile == "" ? "https://media.istockphoto.com/id/1300845620/vector/user-icon-flat-isolated-on-white-background-user-symbol-vector-illustration.jpg?s=612x612&w=0&k=20&c=yBeyba0hUkh14_jgv1OKqIH0CCSWU_4ckRkAoy2p73o=" : avatarFile;
                }
            }
            movie.Language = request.Language;
            var movietype = dbContext.movieTypes.Find(int.Parse(request.MovieTypeId.ToString()));
            if (movietype == null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "MovieTypeId không hợp lệ ! ", null);
            movie.MovieTypeId = request.MovieTypeId;
            movie.Name = request.Name;
            var rate = dbContext.movieTypes.Find(int.Parse(request.RateId.ToString()));
            if (rate == null) return responseObject.ResponseError(StatusCodes.Status400BadRequest, "RateId không hợp lệ ! ", null);
            movie.RateId = request.RateId;
            movie.Trailer = request.Trailer;
            movie.IsActive = request.IsActive;
            dbContext.movies.Update(movie);
            await dbContext.SaveChangesAsync();
            return responseObject.ResponseSuccess("Sửa Movie thành công !", converterMovie.EntityToDTO(movie));
        }

        public IQueryable<DTO_Movie> ViewMovie_Cinema_Room_Seat(Request_inputMovie? request, int pageSize, int pageNumber)
        {
            var query = dbContext.movies
                .Where(x => x.IsActive == true) 
                .Include(x => x.Schedules.Where(s => s.IsActive == true))
                 .ThenInclude(y => y.Room) 
                     .ThenInclude(z => z.Seats) 
                         .ThenInclude(ss => ss.SeatStatus)
                          .AsNoTracking()
                .AsQueryable();

            if (request.CinemaId.HasValue)
            {
                query = query.Where(x => x.Schedules.Any(s => s.Room.CinemaId == request.CinemaId.Value));
            }

            if (request.RoomId.HasValue)
            {
                query = query.Where(x => x.Schedules.Any(s => s.RoomId == request.RoomId.Value && s.IsActive==true));
            }

            if (request.SeatStatusId.HasValue)
            {
                query = query.Where(x => x.Schedules.Any(s => s.Room.Seats.Any(seat => seat.SeatStatusId == request.SeatStatusId.Value)));
            }
            query= query.Skip((pageNumber-1)*pageSize).Take(pageSize);
            return query.Select(x=>converterMovie.EntityToDTO(x));





        }
    }
    }
