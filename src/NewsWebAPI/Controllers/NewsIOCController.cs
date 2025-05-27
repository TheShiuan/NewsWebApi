using Microsoft.AspNetCore.Mvc;
using NewsWebAPI.Dtos;
using NewsWebAPI.Interfaces;
using NewsWebAPI.Parameters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsIOCController : ControllerBase
    {
        private readonly IEnumerable<INewsServices> _newsServices;
        public NewsIOCController(IEnumerable<INewsServices> newsServices)
        {
            _newsServices = newsServices;
        }
        // GET: api/<NewsIOCController>
        [HttpGet]
        public List<NewsGetDto> Get([FromQuery] NewsSelectParamter value)
        {
            INewsServices _news;
            if (value.type == "fun")
            {
                _news = _newsServices.Where(a => a.type == "fun").Single();
            }
            else
            {
                _news = _newsServices.Where(a => a.type == "autoMapper").Single();
            }

            return _news.GetData(value);
        }
    }
}
