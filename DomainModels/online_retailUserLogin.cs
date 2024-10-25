﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class online_retailUserLogin
    {
        public string user_id { get; set; } // Maps to user_id
        public string user_password { get; set; } // Maps to user_password
        public string first_name { get; set; } // Maps to first_name
        public string last_name { get; set; } // Maps to last_name
        public DateTime sign_up_on { get; set; } // Maps to sign_up_on
        public string email_id { get; set; } // Maps to email_id
    }
}
