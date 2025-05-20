using NewsWebAPI.Abstracts;

namespace NewsWebAPI.Dtos
{
    public class NewsPutDto : NewsDtoAbstracts
    {
        public Guid NewsId { get; set; }
    }
}
