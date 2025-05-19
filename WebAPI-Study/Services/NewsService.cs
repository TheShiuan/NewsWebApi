using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using News.Dtos;
using News.Models;
using News.Parameters;

namespace News.Services
{
    public class NewsService
    {
        private readonly WebContext _webContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NewsService(WebContext webContext, IMapper autoMapper, IHttpContextAccessor httpContextAccessor)
        {
            _webContext = webContext;
            _mapper = autoMapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public List<NewsGetDto> GetData([FromQuery] NewsSelectParamter value)
        {
            var result = _webContext.News.AsQueryable();

            if (!string.IsNullOrWhiteSpace(value.title))
            {
                result = result.Where(x => x.Title == value.title);
            }
            if (!string.IsNullOrWhiteSpace(value.content))
            {
                result = result.Where(x => x.Content.Contains(value.content));
            }
            if (value.InsertDateTime != null)
            {
                result = result.Where(x => x.InsertDateTime.Date == ((DateTime)value.InsertDateTime).Date);
            }
            if (value.minClick != null && value.maxClick != null)
            {
                result = result.Where(x => x.Click >= value.minClick && x.Click <= value.maxClick);
            }

            return result.ToList().Select(ItemToNewsDto).ToList();
        }
        public NewsGetDto GetOneData(Guid NewsId)
        {
            var result = (from news in _webContext.News
                          where news.NewsId == NewsId
                          select ItemToNewsDto(news)).SingleOrDefault();
            if (result == null)
                return null;

            return result;
        }
        public IEnumerable<NewsGetDto> AutoMapperGetData([FromQuery] NewsSelectParamter value)
        {
            var result = from news in _webContext.News
                         select news;

            if (!string.IsNullOrWhiteSpace(value.title))
            {
                result = result.Where(x => x.Title == value.title);
            }
            if (!string.IsNullOrWhiteSpace(value.content))
            {
                result = result.Where(x => x.Content.Contains(value.content));
            }
            if (value.InsertDateTime != null)
            {
                result = result.Where(x => x.StartDateTime.Date == ((DateTime)value.InsertDateTime).Date);
            }
            if (value.minClick != null && value.maxClick != null)
            {
                result = result.Where(x => x.Click >= value.minClick && x.Click <= value.maxClick);
            }
            return _mapper.Map<IEnumerable<NewsGetDto>>(result);
        }
        public News InsertData([FromBody] NewsPostDto value)
        {
            //var Claim = _httpContextAccessor.HttpContext.User.Claims.ToList();

            //var employeeid = Claim.Where(a => a.Type == "EmployeeId").First().Value;

            var employeeid = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            News insert = new News()
            {
                Title = value.Title,
                Content = value.Content,
                Click = value.Click,
                InsertDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now,
                InsertEmployeeId = Guid.Parse(employeeid),
                UpdateEmployeeId = Guid.Parse(employeeid),
            };
            _webContext.News.Add(insert);
            _webContext.SaveChanges();

            return insert;
        }
        public void InsertDataAutoMapper([FromBody] NewsPostDto value)
        {
            var map = _mapper.Map<News>(value);
            map.InsertDateTime = DateTime.Now;
            map.UpdateDateTime = DateTime.Now;
            map.InsertEmployeeId = Guid.Parse("1B7EB998-4AC1-4DC5-97AC-091FD60784BD");
            map.UpdateEmployeeId = Guid.Parse("1B7EB998-4AC1-4DC5-97AC-091FD60784BD");

            _webContext.News.Add(map);
            _webContext.SaveChanges();
        }
        public int UpdateData(Guid id, [FromBody] NewsPutDto value)
        {
            var update = (from a in _webContext.News
                          where a.NewsId == id
                          select a).SingleOrDefault();

            if (update != null)
            {
                update.UpdateDateTime = DateTime.Now;
                update.UpdateEmployeeId = Guid.Parse("1B7EB998-4AC1-4DC5-97AC-091FD60784BD");

                _webContext.News.Update(update).CurrentValues.SetValues(value);
            }
            return _webContext.SaveChanges();
        }
        public int DeleteData(Guid id)
        {
            var delete = (from a in _webContext.News
                          where a.NewsId == id
                          select a).SingleOrDefault();

            if (delete != null)
            {
                _webContext.News.Remove(delete);
            }
            return _webContext.SaveChanges();
        }
        private static NewsGetDto ItemToNewsDto(News item)
        {
            return new NewsGetDto
            {
                NewsId = item.NewsId,
                Title = item.Title,
                Content = item.Content,
                InsertEnployId = item.InsertEmployeeId,
                UpdateEnployId = item.UpdateEmployeeId,
                InsertDateTime = item.InsertDateTime,
                UpdateDateTime = item.UpdateDateTime,
                Click = item.Click
            };
        }
    }
}
