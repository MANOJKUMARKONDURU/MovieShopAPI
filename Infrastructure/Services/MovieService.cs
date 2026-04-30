using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetTopMoviesAsync(int count)
        {
            var movies = await _movieRepository.GetTopMoviesAsync(count);

            return movies.Select(m => new MovieCardResponseModel
            {
                Id = m.Id,
                Title = m.Title,
                PosterUrl = m.PosterUrl
            });
        }

        public async Task<MovieDetailsModel> GetMovieDetailsAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null) return null;

            var model = new MovieDetailsModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                Rating = movie.Rating ?? 0
            };

            model.Genres = movie.MovieGenres
                .Select(mg => mg.Genre.Name)
                .ToList();

            model.Trailers = movie.Trailers
                .Select(t => new TrailerModel
                {
                    Name = t.Name,
                    Url = t.TrailerUrl
                })
                .ToList();


            model.Casts = movie.MovieCasts
                .Select(mc => new MovieCastModel
                {
                    CastId = mc.CastId,
                    Name = mc.Cast.Name,
                    Character = mc.Character,
                    ProfilePath = mc.Cast.ProfilePath
                })
                .ToList();


            return model;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenreAsync(int genreId)
        {
            var movies = await _movieRepository.GetMoviesByGenreAsync(genreId);

            return movies.Select(m => new MovieCardResponseModel
            {
                Id = m.Id,
                Title = m.Title,
                PosterUrl = m.PosterUrl
            });
        }

        public async Task<MoviePagedResultModel> GetPagedMoviesAsync(int pageNumber, int pageSize)
        {
            var movies = await _movieRepository.GetMoviesPagedAsync(pageNumber, pageSize);
            var totalCount = await _movieRepository.GetTotalMoviesCountAsync();

            return new MoviePagedResultModel
            {
                Movies = movies.Select(m => new MovieCardResponseModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl
                }),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalMovies = totalCount
            };
        }

        public async Task<MoviePagedResultModel> GetPagedMoviesByGenreAsync(int genreId, int pageNumber, int pageSize)
        {
            var movies = await _movieRepository.GetMoviesByGenrePagedAsync(genreId, pageNumber, pageSize);
            var totalCount = await _movieRepository.GetTotalMoviesByGenreCountAsync(genreId);

            return new MoviePagedResultModel
            {
                Movies = movies.Select(m => new MovieCardResponseModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterUrl = m.PosterUrl
                }),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalMovies = totalCount
            };
        }
    }
}
