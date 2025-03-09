using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using News.Dtos;
using News.Models;

namespace News.Filters
{
    public class NewsAuthorizationFilter2 : Attribute, IAuthorizationFilter
    {
        public string Roles = "";
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            WebContext _webContext = context.HttpContext.RequestServices.GetService<WebContext>();

            var role = (from a in _webContext.Roles
                        where a.Role == Roles
                        select a).FirstOrDefault();
            if (role == null)
            {
                context.Result = new JsonResult(new ReturnJson()
                {
                    Data = Roles,
                    HttpCode = 401,
                    ErrorMsg = "沒有登入",
                });
            }

        }
    }
}
