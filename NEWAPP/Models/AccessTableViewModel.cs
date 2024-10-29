namespace NEWAPP.Models
{
    public class AccessTableViewModel
    {
        public int AccessID { get; set; }

        // Foreign key for users
        public int UserID { get; set; }

        // Defines access level (e.g., Admin, Read-Only)
        public string PermissionLevel { get; set; }

        // The resource being accessed (e.g., Module, Feature)
        public string Resource { get; set; }

        // Date when access was granted
        public DateTime GrantedDate { get; set; }

        // Status of access (1 = Active, 0 = Inactive)
        public bool IsActive { get; set; }
    }
}
