using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using News.Dtos;
using News.Models;

namespace News.Filters
{
    public class FileLimitAttribute : Attribute, IResourceFilter
    {
        public long FileSize = 1;
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var files = context.HttpContext.Request.Form.Files;

            foreach (var file in files)
            {
                if (file.Length > (1024 * 1024 * FileSize))
                {
                    context.Result = new JsonResult(new ReturnJson()
                    {
                        Data = "test1",
                        HttpCode = 400,
                        ErrorMsg = "檔案過大",
                    });
                }
                if (Path.GetExtension(file.FileName) != ".txt")
                {
                    context.Result = new JsonResult(new ReturnJson()
                    {
                        Data = "test1",
                        HttpCode = 400,
                        ErrorMsg = "只允許上傳Txt",
                    });
                }
            }
        }
    }
}
