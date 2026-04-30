using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET api/movies/top?count=30
        [HttpGet("top")]
        public async Task<ActionResult<IEnumerable<MovieCardResponseModel>>> GetTopMovies([FromQuery] int count = 30)
        {
            var movies = await _movieService.GetTopMoviesAsync(count);
            return Ok(movies);
        }

        // GET api/movies/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDetailsModel>> GetMovieDetails(int id)
        {
            var movie = await _movieService.GetMovieDetailsAsync(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        // GET api/movies/genre/{genreId}
        [HttpGet("genre/{genreId:int}")]
        public async Task<ActionResult<IEnumerable<MovieCardResponseModel>>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenreAsync(genreId);
            return Ok(movies);
        }

        // GET api/movies/paged?pageNumber=1&pageSize=30
        [HttpGet("paged")]
        public async Task<ActionResult<MoviePagedResultModel>> GetPagedMovies(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 30)
        {
            var result = await _movieService.GetPagedMoviesAsync(pageNumber, pageSize);
            return Ok(result);
        }

        // GET api/movies/genre/{genreId}/paged?pageNumber=1&pageSize=30
        [HttpGet("genre/{genreId:int}/paged")]
        public async Task<ActionResult<MoviePagedResultModel>> GetPagedMoviesByGenre(
            int genreId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 30)
        {
            var result = await _movieService.GetPagedMoviesByGenreAsync(genreId, pageNumber, pageSize);
            return Ok(result);
        }
    }
}
