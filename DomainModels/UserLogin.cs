using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class UserLogin
    {
        public string user_id { get; set; }              // Maps to user_id (nvarchar)
        public string user_password { get; set; }        // Maps to user_password (nullable nvarchar)
        public string first_name { get; set; }           // Maps to first_name (nullable nvarchar)
        public string last_name { get; set; }            // Maps to last_name (nullable nvarchar)
        public DateTime sign_up_on { get; set; }         // Maps to sign_up_on (nullable date)
        public string email_id { get; set; }             // Maps to email_id (nullable nvarchar)
    }
}

