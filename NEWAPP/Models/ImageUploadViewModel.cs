namespace NEWAPP.Models
{
    public class ImageUploadViewModel
    {
        public IFormFile ImageFile { get; set; }
        public string Base64Image { get; set; }
    }
}
