using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Bodyguard
    {
        /// <summary>
        /// Unique identifier for the bodyguard.
        /// </summary>
        public int BodyguardID { get; set; }

        /// <summary>
        /// First name of the bodyguard.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the bodyguard.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Contact number of the bodyguard.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Employment status of the bodyguard (e.g., Active, Inactive).
        /// </summary>
        public string Status { get; set; }
    }
}
