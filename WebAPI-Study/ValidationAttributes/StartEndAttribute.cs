using System.ComponentModel.DataAnnotations;
using News.Dtos;

namespace News.ValidationAttributes
{
    public class StartEndAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var st = (NewsPostDto)value;

            if (st.StartDateTime >= st.EndDateTime)
            {
                return new ValidationResult("開始時間不可以大於結束時間", new string[] { "time" });
            }
            return ValidationResult.Success;
        }
    }
}
