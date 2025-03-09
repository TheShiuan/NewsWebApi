using Microsoft.AspNetCore.Mvc;
using News.Dtos;
using News.Parameters;

namespace News.Interfaces
{
    public interface INewsServices
    {
        string type { get; }
        public List<NewsGetDto> GetData([FromQuery] NewsSelectParamter value);
    }

}
