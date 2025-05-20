using System.ComponentModel.DataAnnotations;
using NewsWebAPI.Dtos;
using NewsWebAPI.Models;

namespace NewsWebAPI.ValidationAttributes
{
    public class NewsTitleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            WebContext _webContext = (WebContext)validationContext.GetService(typeof(WebContext));

            var title = (string)value;

            var findTitle = from a in _webContext.News
                            where a.Title == title
                            select a;

            var dto = validationContext.ObjectInstance;

            if (dto.GetType() == typeof(NewsPutDto))
            {
                var dtoUpdate = (NewsPutDto)dto;
                findTitle = findTitle.Where(a => a.Title != dtoUpdate.Title);
            }

            if (findTitle.Any())
            {
                return new ValidationResult("已存在相同標題新聞");
            }
            return ValidationResult.Success;
        }
    }
}
