using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NEWAPP.Models
{
    public class Author_USViewModel
    {
        [Key] // Primary Key
        public int AuthorID { get; set; }

        [Required] // NOT NULL
        [MaxLength(100)] // NVARCHAR(100)
        public string AuthorName { get; set; }

        public DateTime? BirthDate { get; set; } // Nullable DATE

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] // Default to GETDATE()
        public DateTime CreatedDate { get; set; }
    }
}
