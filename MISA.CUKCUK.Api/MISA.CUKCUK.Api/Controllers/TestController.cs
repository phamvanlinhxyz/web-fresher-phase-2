using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MISA.CUKCUK.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            string fileName;
            try
            {
                var extention = "." + image.FileName.Split('.')[image.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extention;

                var pathBuild = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Images");

                if (!Directory.Exists(pathBuild))
                {
                    Directory.CreateDirectory(pathBuild);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Images", fileName);

                using(var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                 return Ok(1);  
            }
            catch (Exception)
            {

                throw;
            }
                    
        }
    }
}
