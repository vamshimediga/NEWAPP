using System.ComponentModel.DataAnnotations;

namespace NEWAPP.Models
{
    public class BodyguardViewModel
    {
        [Key]
        public int BodyguardID { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(20)]
        public string Status { get; set; }


    }
}
