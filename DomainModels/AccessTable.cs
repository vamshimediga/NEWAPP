using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class AccessTable
    {
        // Auto-incrementing primary key
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
