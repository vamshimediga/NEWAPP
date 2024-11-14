using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
   
    public class Image
    {
        public int ImageId { get; set; }

        
        public string ImageName { get; set; }

        public string ImageData { get; set; } // To store Base64-encoded string


        // To bind the uploaded file
      
        public IFormFile ImageFile { get; set; }
    }
}
