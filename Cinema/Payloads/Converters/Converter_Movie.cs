using Cinema.Entities;
using Cinema.Payloads.DTO;

namespace Cinema.Payloads.Converters
{
    public class Converter_Movie
    {
        public DTO_Movie EntityToDTO(Movie movie)
            => new DTO_Movie
            {
                MovieDuration = movie.MovieDuration,
                EndTime = movie.EndTime,
                PremiereDate = movie.PremiereDate,
                Description = movie.Description,
                Director = movie.Director,
                Image = movie.Image,
                HeroImage = movie.HeroImage,
                Language = movie.Language,
                MovieTypeId = movie.MovieTypeId,
                Name = movie.Name,
                RateId = movie.RateId,
                Trailer = movie.Trailer,
                IsActive = movie.IsActive
            };
    }
}
