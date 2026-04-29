using Microsoft.AspNetCore.Mvc;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/home/get
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Id = 1, Name = "Manoj", Phone = "1234568" });
        }

        // GET: api/home/getbyid/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var people = new[]
            {
                new { Id = 1, Name = "Manoj", Phone = "1234568" },
                new { Id = 2, Name = "Kumar", Phone = "12554568" },
            };

            var person = people.FirstOrDefault(x => x.Id == id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        // POST: api/home/login
        [HttpPost]
        public IActionResult Login([FromBody] string username)
        {
            return Ok(new { Message = $"Welcome {username}!" });
        }

        // POST: api/home/addemployee
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee emp)
        {
            // You can save to DB later — for now just return it
            return Ok(emp);
        }

        // DELETE: api/home/removeemployee/1
        [HttpDelete("{id}")]
        public IActionResult RemoveEmployee(int id)
        {
            return Ok(new { Id = id, Name = "Deleted", Phone = "00000000000" });
        }
    }
}