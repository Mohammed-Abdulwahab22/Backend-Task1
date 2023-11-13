using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Task1_updated.Models;

namespace Task1_updated.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Upload : ControllerBase
    {
        [HttpPost("operations")]
        public IActionResult UploadFile([FromForm] FileContent model)
        {
            var fileExtension = model.File.ContentType == "image/jpeg" ? "jpg" : "mp4";

            if (FileExists(model.FileName, model.Owner, fileExtension))
            {
                return BadRequest("File already exists.");
            }
            if (model.File.ContentType != "image/jpeg" && model.File.ContentType != "video/mp4")
            {
                return BadRequest("File type not supported. Please upload a jpg or mp4 file.");
            }


            var filePath = Path.Combine("Files", $"{model.FileName}.{fileExtension}");
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.File.CopyTo(stream);
            }

            var jsonContent = new
            {
                Filename = model.FileName,
                Owner = model.Owner,
                Description = model.Description
            };
            var jsonFilePath = Path.Combine("Files", $"{model.FileName}.json");
            System.IO.File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(jsonContent));



            return Ok("File created successfully.");



        }
        private bool FileExists(string fileName, string owner, string fileExtension)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", $"{fileName}.{fileExtension}");
            return System.IO.File.Exists(filePath);
        }
    }
}
