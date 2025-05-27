using Microsoft.AspNetCore.Mvc.Filters;

namespace NewsWebAPI.Filters
{
    public class NewsActionFilter : IActionFilter
    {

        private readonly IWebHostEnvironment _env;
        public NewsActionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            string filePath = _env.ContentRootPath + @"\Log\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var employeeid = context.HttpContext.User.FindFirst("EmployeeId");
            var path = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;
            string text = "結束: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "path: " + path + "method: " + method + "" + employeeid + "\n";
            File.AppendAllText(filePath + DateTime.Now.ToString("yyyMMdd") + ".txt", text);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string filePath = _env.ContentRootPath + @"\Log\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            var employeeid = context.HttpContext.User.FindFirst("EmployeeId");
            var path = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;
            string text = "開始: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "path: " + path + "method: " + method + "" + employeeid + "\n";
            File.AppendAllText(filePath + DateTime.Now.ToString("yyyyMMdd") + ".txt", text);
        }
    }
}
