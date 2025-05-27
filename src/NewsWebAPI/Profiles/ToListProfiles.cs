using AutoMapper;
using NewsWebAPI.Dtos;
using NewsWebAPI.Models;

namespace NewsWebAPI.Profiles
{
    public class ToListProfiles : Profile
    {
        public ToListProfiles()
        {
            CreateMap<News, NewsGetDto>()
                .ForMember(dest => dest.Title,
                opt => opt.MapFrom(src => src.Title + "(use autoMapper)")
                ); ;
            CreateMap<NewsPostDto, News>();
            CreateMap<NewsPutDto, News>()
                .ForMember(dest => dest.UpdateDateTime,
                opt => opt.MapFrom(src => DateTime.Now)
                )
                .ForMember(dest => dest.UpdateEmployeeId,
                opt => opt.MapFrom(src => Guid.Parse("1B7EB998-4AC1-4DC5-97AC-091FD60784BD"))
                );

        }
    }
}
