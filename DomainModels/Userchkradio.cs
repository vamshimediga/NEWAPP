using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Userchkradio
    {
        public int UserID { get; set; }
        public char Gender { get; set; }
        public bool EmailNotifications { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
