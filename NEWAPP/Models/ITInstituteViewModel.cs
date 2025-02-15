using System.ComponentModel.DataAnnotations;

namespace NEWAPP.Models
{
    public class ITInstituteViewModel
    {
        public int InstituteID { get; set; } // Unique identifier for the institute

        [Display(Name = "Institute Name")]
        public string InstituteName { get; set; } // Name of the institute

        [Display(Name = "Location")]
        public string Location { get; set; } // Location of the institute

        [Display(Name = "Contact Number")]
        [RegularExpression(@"\d{3}-\d{3}-\d{4}", ErrorMessage = "Please enter a valid phone number in the format 111-111-2323.")]
        public string ContactNumber { get; set; } // Contact number of the institute

        [Display(Name = "Established Year")]
        public int? EstablishedYear { get; set; } // Year the institute was established (nullable)

        [Display(Name = "Courses Offered")]
        public string CoursesOffered { get; set; } // List of courses offered

        [Display(Name = "Rating")]
        public string FormattedRating { get; set; } // Display-friendly rating (e.g., "4.5 / 5")

        [Display(Name = "Created Date")]
        public string CreatedDateFormatted { get; set; } // Display-friendly created date

        [Display(Name = "Modified Date")]
        public string ModifiedDateFormatted { get; set; } // Display-friendly modified date
    }
}
