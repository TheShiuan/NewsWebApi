using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using News.Filters;
using News.Interfaces;
using News.Models;
using News.Services;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WebContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("WebDatabase")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<NewsService>();
builder.Services.AddScoped<NewsAsyncService>();

builder.Services.AddScoped<INewsServices, NewsLinqService>();
builder.Services.AddScoped<INewsServices, NewsMapperService>();

builder.Services.AddHttpContextAccessor();

// Cookies ��������
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
//{
//    //���n�J�ɷ|�۰ʾɨ�o�Ӻ��}
//    option.LoginPath = new PathString("/api/Login/NoLogin");
//    //�S���v���ɷ|�۰ʾɨ�o�Ӻ��}
//    option.AccessDeniedPath = new PathString("/api/Login/NoAccess");
//    //�]�w�n�J�ɭ���1����
//    option.ExpireTimeSpan = TimeSpan.FromMinutes(1);
//});
//builder.Services.AddMvc(options =>
//{
//    options.Filters.Add(new AuthorizeFilter());
//});

//JWT ��������
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuer = true,
          ValidIssuer = Configuration["Jwt:Issuer"],
          ValidateAudience = true,
          ValidAudience = Configuration["Jwt:Audience"],
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:KEY"]))
      };
  });

builder.Services.AddMvc(option =>
{
    option.Filters.Add(new AuthorizeFilter());

    option.Filters.Add(typeof(NewsActionFilter));

    option.Filters.Add(typeof(NewsResultFilter));

    //option.Filters.Add(new NewsAuthorizationFilter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

//���ǭn�@��
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
