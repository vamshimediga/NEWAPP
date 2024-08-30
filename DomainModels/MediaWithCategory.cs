using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class MediaWithCategory
    {
        [Required(ErrorMessage = "CategoryID is required.")]
        public int CategoryID { get; set; } // Represents CategoryID from MediaCategory

        public string CategoryName { get; set; } // Represents CategoryName from MediaCategory

    
        public int MediaID { get; set; } // Represents MediaID from Media

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; } // Represents Title from Media

        [Required(ErrorMessage = "MediaType is required.")]
        [StringLength(50, ErrorMessage = "MediaType cannot exceed 50 characters.")]
        public string MediaType { get; set; } // Represents MediaType from Media

        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime? ReleaseDate { get; set; } // Represents ReleaseDate from Media

        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10.")]
        public decimal? Rating { get; set; } // Represents Rating from Media
    }

}
