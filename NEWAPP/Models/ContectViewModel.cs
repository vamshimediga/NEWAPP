namespace NEWAPP.Models
{
    public class ContectViewModel
    {
        // Auto-incremented unique ID (Primary Key)
        public int ContectID { get; set; }

        // First Name of the contact (Required)
        public string FirstName { get; set; }

        // Last Name of the contact (Required)
        public string LastName { get; set; }

        // Contact's email (must be unique and required)
        public string Email { get; set; }

        // Phone number of the contact (Optional)
        public string PhoneNumber { get; set; }

        // Contact's address (Optional)
        public string Address { get; set; }

        // Timestamp for when the contact is created (Defaults to current date/time)
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Timestamp for last modification (Defaults to current date/time)
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
