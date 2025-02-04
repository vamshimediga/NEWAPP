using DomainModels;

namespace NEWAPP.Models
{
    public class UsersListViewModel
    {
        public int UserID { get; set; }  // Primary Key
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

      //  public UserRolesViewModel UserRole { get; set; }
        // Foreign Key
        public int RoleID { get; set; }

        // Navigation Property for Relationship
        public ICollection<UserRolesViewModel> Roles { get; set; }
    }
}
