using DomainModels;

namespace NEWAPP.Models
{
    public class UserDataViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserProfile Profile { get; set; } // Represents the One-to-One relationship with UserProfile
    }
}
