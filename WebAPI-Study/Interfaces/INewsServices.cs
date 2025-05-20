using Microsoft.AspNetCore.Mvc;
using NewsWebAPI.Dtos;
using NewsWebAPI.Parameters;

namespace NewsWebAPI.Interfaces
{
    public interface INewsServices
    {
        string type { get; }
        public List<NewsGetDto> GetData([FromQuery] NewsSelectParamter value);
    }

}
