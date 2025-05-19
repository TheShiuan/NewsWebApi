using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using News.Dtos;
using News.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace News.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly WebContext _webContext;
        private readonly IConfiguration _configuration;

        public LoginController(WebContext webContext, IConfiguration configuration)
        {
            _webContext = webContext;
            _configuration = configuration;
        }

        [HttpPost]
        public string login(LoginPostDto value)
        {
            var user = (from a in _webContext.Employees
                        where a.Account == value.Account
                        && a.Password == value.Password
                        select a).SingleOrDefault();

            if (user == null)
            {
                return "帳號密碼錯誤";
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Account),
                    new Claim("FullName", user.Name),
                    new Claim("EmployeeId",user.EmployeesId.ToString())
                };

                var role = from a in _webContext.Roles
                           where a.EmployeesId == user.EmployeesId
                           select a;

                foreach (var temp in role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, temp.Role1));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return "ok";
            }
        }
        [HttpPost("jwtLogin")]
        public string jwtLogin(LoginPostDto value)
        {
            var user = (from a in _webContext.Employees
                        where a.Account == value.Account
                        && a.Password == value.Password
                        select a).SingleOrDefault();

            if (user == null)
            {
                return "帳號密碼錯誤";
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Account),
                    new Claim("FullName", user.Name),
                    new Claim(JwtRegisteredClaimNames.NameId,user.EmployeesId.ToString())
                };

                var role = from a in _webContext.Roles
                           where a.EmployeesId == user.EmployeesId
                           select a;

                foreach (var temp in role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, temp.Role1));
                }

                //取得 appsettings jwt key
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                //jwt 相關設定
                var jwt = new JwtSecurityToken
                (
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(2),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );

                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                return token;
            }
        }

        [HttpDelete]
        public void logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        [HttpGet("NoLogin")]
        public string noLogin()
        {
            return "未登入";
        }
        [HttpGet("NoAccess")]
        public string noAccess()
        {
            return "沒有權限";
        }
    }
}
