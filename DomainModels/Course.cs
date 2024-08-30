using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Course
    {
        public int CourseID { get; set; }  // Primary Key

        [Required]
        [StringLength(100)]
        public string CourseName { get; set; }  // Course name with a maximum length of 100 characters

        [StringLength(255)]
        public string CourseDescription { get; set; }  // Course description with a maximum length of 255 characters

        [Required]
        public int Credits { get; set; }  // Number of credits for the course

        public int? DepartmentID { get; set; }  // Nullable DepartmentID, as it can be NULL

        public DateTime CreatedDate { get; set; } = DateTime.Now;  // Default to current date and time

        public DateTime? ModifiedDate { get; set; }  // Nullable ModifiedDate, as it can be NULL
    }

}
