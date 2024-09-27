using System.ComponentModel.DataAnnotations;

namespace NEWAPP.Models
{
    public class LeadViewModel
    {
        public int LeadID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number")]
        [StringLength(15, ErrorMessage = "Phone Number cannot exceed 15 characters")]
        public string PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "Lead Source cannot exceed 50 characters")]
        public string LeadSource { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters")]
        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }

}
