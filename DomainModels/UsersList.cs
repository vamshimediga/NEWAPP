using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class UsersList
    {
        public int UserID { get; set; }  // Primary Key
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        
        // Foreign Key
        public int RoleID { get; set; }


        // Navigation Property for Relationship
        public ICollection<UserRoles> Roles { get; set; }
    }
}
