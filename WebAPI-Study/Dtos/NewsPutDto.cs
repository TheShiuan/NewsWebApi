using News.ValidationAttributes;
using WebAPI_Study.Abstracts;

namespace News.Dtos
{
    public class NewsPutDto : NewsDtoAbstracts
    {
        public Guid NewsId { get; set; }
    }
}
