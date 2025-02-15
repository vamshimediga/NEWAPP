using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEWAPP.Models
{
    public class ImageViewModel
    {
        public int ImageId { get; set; }

        [Required]
        public string ImageName { get; set; }
        [BindNever]
        public string ImageData { get; set; } = string.Empty; // To store Base64-encoded string


        // To bind the uploaded file
        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile ImageFile { get; set; }
    }
}
