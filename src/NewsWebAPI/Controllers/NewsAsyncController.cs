using Microsoft.AspNetCore.Mvc;
using NewsWebAPI.Dtos;
using NewsWebAPI.Parameters;
using NewsWebAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAsyncController : ControllerBase
    {
        private readonly NewsAsyncService _newsAsyncService;

        public NewsAsyncController(NewsAsyncService newsAsyncService)
        {
            _newsAsyncService = newsAsyncService;
        }

        // GET: api/<NewsAsyncController>
        [HttpGet]
        public async Task<List<NewsGetDto>> Get([FromQuery] NewsSelectParamter value)
        {
            return await _newsAsyncService.GetData(value);
        }

        // GET api/<NewsAsyncController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NewsAsyncController>
        [HttpPost]
        public async void Post([FromBody] NewsPostDto value)
        {
            await _newsAsyncService.InsertData(value);
        }

        // PUT api/<NewsAsyncController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NewsAsyncController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
