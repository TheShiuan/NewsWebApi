using Microsoft.AspNetCore.Mvc;
using News.Dtos;
using News.Interfaces;
using News.Models;
using News.Parameters;

namespace News.Services
{
    public class NewsLinqService : INewsServices
    {
        private readonly WebContext _webContext;

        public string type => "fun";

        public NewsLinqService(WebContext webContext)
        {
            _webContext = webContext;
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
        private static NewsGetDto ItemToNewsDto(News item)
        {
            return new NewsGetDto
            {
                NewsId = item.NewsId,
                Title = item.Title + "(use fun)",
                Content = item.Content,
                InsertDateTime = item.InsertDateTime,
                UpdateDateTime = item.UpdateDateTime,
                Click = item.Click
            };
        }
    }
}
