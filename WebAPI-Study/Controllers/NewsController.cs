using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using News.Dtos;
using News.Models;
using News.Parameters;
using News.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly WebContext _webContext;
        private readonly IMapper _mapper;
        private readonly NewsService _newsServices;

        public NewsController(WebContext webContext, IMapper mapper, NewsService newsServices)
        {
            _webContext = webContext;
            _mapper = mapper;
            _newsServices = newsServices;
        }

        // GET: api/<NewsController>
        [HttpGet]
        //[NewsAuthorizationFilter2(Roles = "select")]
        //[Authorize(Roles = "select")]
        public IActionResult Get([FromQuery] NewsSelectParamter value)
        {
            var result = _newsServices.GetData(value);
            if (result == null || result.Count() <= 0)
            {
                return NotFound("找不到資源");
            }

            return Ok(result);
        }
        [HttpGet("AutoMapper")]
        //[NewsAuthorizationFilter2(Roles = "autoMapper")]
        //[Authorize(Roles = "autoMapper")]
        public IEnumerable<NewsGetDto> GetAutoMapper([FromQuery] NewsSelectParamter value)
        {
            return _newsServices.AutoMapperGetData(value);
        }

        // GET api/<NewsController>/5
        [HttpGet("{id}")]
        public ActionResult<NewsGetDto> Get([FromRoute] Guid id)
        {
            var result = _newsServices.GetOneData(id);
            if (result == null)
                return NotFound($"id : {id} is not found");

            return Ok(result);
        }
        [HttpGet("AutoMapper/{id}")]
        public NewsGetDto GetAutoMapper([FromRoute] Guid id)
        {
            var result = (from news in _webContext.News
                          where news.NewsId == id
                          select news).SingleOrDefault();
            return _mapper.Map<NewsGetDto>(result);
        }

        // POST api/<NewsController>
        [HttpPost]
        public IActionResult Post([FromBody] NewsPostDto value)
        {
            var insert = _newsServices.InsertData(value);

            return Ok(insert);
        }
        [HttpPost("AutoMapper")]
        public void PostAutoMapper([FromBody] NewsPostDto value)
        {
            _newsServices.InsertDataAutoMapper(value);
        }

        // PUT api/<NewsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] NewsPutDto value)
        {
            if (id != value.NewsId)
            {
                return BadRequest();
            }

            if (_newsServices.UpdateData(id, value) == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut("AutoMapper/{id}")]
        public void PutAutoMapper(Guid id, [FromBody] NewsPutDto value)
        {
            var update = (from a in _webContext.News
                          where a.NewsId == id
                          select a).SingleOrDefault();

            if (update != null)
            {
                _mapper.Map(value, update);
                _webContext.SaveChanges();
            }
        }
        [HttpPatch("{id}")]
        public void Patch(Guid id, [FromBody] JsonPatchDocument value)
        {
            var update = (from a in _webContext.News
                          where a.NewsId == id
                          select a).SingleOrDefault();

            if (update != null)
            {
                update.UpdateDateTime = DateTime.Now;
                update.UpdateEmployeeId = Guid.Parse("1B7EB998-4AC1-4DC5-97AC-091FD60784BD");

                value.ApplyTo(update);
                _webContext.SaveChanges();
            }
        }
        // DELETE api/<NewsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (_newsServices.DeleteData(id) == 0)
            {
                return NotFound("找不到該資料");
            }
            return NoContent();
        }
        [HttpDelete("list/{ids}")]
        public IActionResult DeleteList(string ids)
        {

            List<Guid> deleteList = JsonSerializer.Deserialize<List<Guid>>(ids);

            if (deleteList == null || !deleteList.Any())
            {
                return BadRequest("No valid IDs provided.");
            }

            var delete = from a in _webContext.News
                         where deleteList.Contains(a.NewsId)
                         select a;

            if (!delete.Any())
            {
                return NotFound("No matching records found.");
            }

            _webContext.News.RemoveRange(delete);
            _webContext.SaveChanges();

            return Ok("Records deleted successfully.");

        }
    }
}
