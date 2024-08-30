using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class DefectiveProducts
    {
        public int DefectiveProductID { get; set; }
        public int ProductID { get; set; }
        public string DefectDescription { get; set; }
    }
}
