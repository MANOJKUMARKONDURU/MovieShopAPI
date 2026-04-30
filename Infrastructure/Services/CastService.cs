using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetailsAsync(int id)
        {
            var cast = await _castRepository.GetCastWithMoviesAsync(id);
            if (cast == null) return null;

            var model = new CastDetailsModel
            {
                Id = cast.Id,
                Name = cast.Name,
                ProfilePath = cast.ProfilePath
            };

            model.Movies = cast.MovieCasts
                .Select(mc => new CastMovieModel
                {
                    MovieId = mc.MovieId,
                    Title = mc.Movie.Title,
                    Character = mc.Character
                })
                .ToList();

            return model;
        }
    }
}