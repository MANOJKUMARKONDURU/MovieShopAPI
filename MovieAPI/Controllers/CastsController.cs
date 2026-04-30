using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CastsController : ControllerBase
    {
        private readonly ICastService _castService;

        public CastsController(ICastService castService)
        {
            _castService = castService;
        }

        // GET api/casts/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CastDetailsModel>> GetCastDetails(int id)
        {
            var cast = await _castService.GetCastDetailsAsync(id);
            if (cast == null) return NotFound();
            return Ok(cast);
        }
    }
}