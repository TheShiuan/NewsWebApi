using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using News.Dtos;

namespace News.Filters
{
    public class NewsAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool tokenFlag = context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues outValue);

            var ignore = (from a in context.ActionDescriptor.EndpointMetadata
                          where a.GetType() == typeof(AllowAnonymousAttribute)
                          select a).FirstOrDefault();

            if (ignore == null)
            {

                if (tokenFlag)
                {
                    if (outValue != "123")
                    {
                        context.Result = new JsonResult(new ReturnJson()
                        {
                            Data = "test1",
                            HttpCode = 401,
                            ErrorMsg = "沒有登入",
                        });
                        context.Result = new UnauthorizedResult();
                    }

                }
                else
                {
                    context.Result = new JsonResult(new ReturnJson()
                    {
                        Data = "test2",
                        HttpCode = 401,
                        ErrorMsg = "沒有登入",
                    });
                }
            }
        }
    }
}
