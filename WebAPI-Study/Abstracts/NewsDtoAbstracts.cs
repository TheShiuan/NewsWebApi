using System.ComponentModel.DataAnnotations;
using NewsWebAPI.Dtos;
using NewsWebAPI.Models;

namespace NewsWebAPI.Abstracts
{
    public abstract class NewsDtoAbstracts : IValidatableObject
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Click { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            WebContext _webContext = (WebContext)validationContext.GetService(typeof(WebContext));

            var findTitle = from a in _webContext.News
                            where a.Title == Title
                            select a;

            if (this.GetType() == typeof(NewsPutDto))
            {
                var dtoUpdate = (NewsPutDto)this;
                findTitle = findTitle.Where(a => a.NewsId != dtoUpdate.NewsId);
            }

            if (findTitle.FirstOrDefault() != null)
            {
                yield return new ValidationResult("已存在相同標題新聞", new string[] { "Title" });
            }

            if (StartDateTime > EndDateTime)
            {
                yield return new ValidationResult("開始時間不可以大於結束時間", new string[] { "Time" });
            }
        }
    }
}
