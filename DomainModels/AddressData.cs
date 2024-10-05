using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class AddressData
    {
        public int AddressID { get; set; }
        public int PersonID { get; set; }  // Foreign Key
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
