using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NEWAPP.Models;

namespace NEWAPP.Controllers
{
    public class ImageController : Controller
    {
        // GET: ImageController
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult ImageUpload()
        {
            return View(new ImageUploadViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(ImageUploadViewModel model)
        {
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ImageFile.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    model.Base64Image = Convert.ToBase64String(imageBytes);
                }
            }
            return View("ImageUpload", model);
        }



    }
}
