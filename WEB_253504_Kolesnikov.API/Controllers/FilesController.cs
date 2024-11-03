using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WEB_253504_Kolesnikov.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly string _imagePath;

        public FilesController(IWebHostEnvironment webHost)
        {
            _imagePath = Path.Combine(webHost.WebRootPath, "Images");
        }

        [HttpPost]
        public async Task<IActionResult> SaveFile(IFormFile file)
        {
            if (file == null)
            {
                return BadRequest();
            }
            // Path to downloading file
            var filePath = Path.Combine(_imagePath, file.FileName);
            var fileInfo = new FileInfo(filePath);
            //if this file exists - delete it
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            //copy file in thread
            using var fileStream = fileInfo.Create();
            await file.CopyToAsync(fileStream);

            //get url
            var host = HttpContext.Request.Host;
            var fileUrl = $"Https://{host}/Images/{file.FileName}";

            return Ok(fileUrl);
        }

        [HttpDelete]
        public IActionResult DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_imagePath, fileName);
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists) 
            {
                fileInfo.Delete();
                return Ok(new { message = "File deleted successfully" });
            }
            else
            {
                return NotFound(new { message = "File not found"});
            }
        }
    }
}
