namespace NEWAPP.Models
{
    public class ITInstituteViewModel
    {
        public int InstituteID { get; set; } // Unique identifier for the institute
        public string InstituteName { get; set; } // Name of the institute
        public string Location { get; set; } // Location of the institute
        public string ContactNumber { get; set; } // Contact number of the institute
        public int? EstablishedYear { get; set; } // Year the institute was established (nullable)
        public string CoursesOffered { get; set; } // List of courses offered
        public string FormattedRating { get; set; } // Display-friendly rating (e.g., "4.5 / 5")
        public string CreatedDateFormatted { get; set; } // Display-friendly created date
        public string ModifiedDateFormatted { get; set; } // Display-friendly modified date
        
    }
}
