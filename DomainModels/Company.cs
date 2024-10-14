using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
