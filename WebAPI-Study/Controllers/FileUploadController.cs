using Microsoft.AspNetCore.Mvc;
using News.Filters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Study.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public FileUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }
        // POST api/<FileUploadController>
        [HttpPost]
        [FileLimit(FileSize = 1)]
        public void Post(List<IFormFile> files)
        {
            string filePath = _env.ContentRootPath + @"\wwwroot\";
            foreach (var file in files)
            {
                string fileName = file.FileName;
                using (var stream = System.IO.File.Create(filePath + fileName))
                {
                    file.CopyTo(stream);
                }
            }
        }
        [HttpPost("{id}")]
        [FileLimit(FileSize = 1)]
        public void Post(List<IFormFile> files, Guid id)
        {
            string filePath = _env.ContentRootPath + @"\wwwroot\UploadFiles\" + id + "\\";
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            foreach (var file in files)
            {
                string fileName = file.FileName;
                using (var stream = System.IO.File.Create(filePath + fileName))
                {
                    file.CopyTo(stream);
                }
            }
        }
    }
}
