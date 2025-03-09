using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using News.Dtos;

namespace News.Filters
{
    public class NewsResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var contextResult = context.Result as ObjectResult;

            if (context.ModelState.IsValid)
            {
                context.Result = new JsonResult(new ReturnJson()
                {
                    Data = contextResult.Value
                });
            }
            else
            {
                context.Result = new JsonResult(new ReturnJson()
                {
                    ErrorMsg = contextResult.Value
                });
            }

        }
    }
}
