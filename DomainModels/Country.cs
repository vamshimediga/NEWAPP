using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
  
    public class Country
    {
        public Name name { get; set; }
        public string capital { get; set; }
        public string flag { get; set; }
    }

    public class Name
    {
        public string common { get; set; }
        public string official { get; set; }
    }

}
