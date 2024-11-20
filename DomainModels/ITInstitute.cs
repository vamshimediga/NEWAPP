using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class ITInstitute
    {
        public int InstituteID { get; set; } // Unique identifier for the institute
        public string InstituteName { get; set; } // Name of the institute
        public string Location { get; set; } // Location of the institute
        public string ContactNumber { get; set; } // Contact number of the institute
        public int? EstablishedYear { get; set; } // Year the institute was established (nullable)
        public string CoursesOffered { get; set; } // List of courses offered (comma-separated)
        public decimal? Rating { get; set; } // Rating out of 5.00 (nullable)
        public DateTime CreatedDate { get; set; } // Record creation date
        public DateTime? ModifiedDate { get; set; } // Record modification date (nullable)
    }
}
