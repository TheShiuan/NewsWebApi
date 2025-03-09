using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Study.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolloWorldController : ControllerBase
    {
        // GET: api/<HollowWorld>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "hello", "world" };
        }

        // GET api/<HollowWorld>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HollowWorld>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HollowWorld>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HollowWorld>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
