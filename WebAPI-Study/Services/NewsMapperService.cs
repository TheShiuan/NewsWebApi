using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using News.Dtos;
using News.Interfaces;
using News.Models;
using News.Parameters;

namespace News.Services
{
    public class NewsMapperService : INewsServices
    {
        public string type => "autoMapper";
        private readonly WebContext _webContext;
        private readonly IMapper _mapper;
        public NewsMapperService(WebContext webContext, IMapper autoMapper)
        {
            _webContext = webContext;
            _mapper = autoMapper;
        }


        public List<NewsGetDto> GetData([FromQuery] NewsSelectParamter value)
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
            return _mapper.Map<IEnumerable<NewsGetDto>>(result).ToList();
        }
    }
}
