using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class UserData
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserProfile Profile { get; set; } // Represents the One-to-One relationship with UserProfile
    }
}
