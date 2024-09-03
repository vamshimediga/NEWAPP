using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DomainModels
{
    public class FileUploadModel
    {
        public string Description { get; set; }
        public IFormFile File { get; set; }
    }

}
