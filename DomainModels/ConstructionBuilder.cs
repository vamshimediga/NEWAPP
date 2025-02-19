using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class ConstructionBuilder
    {
        public int BuilderID { get; set; }
        public string BuilderName { get; set; }
        public string CompanyName { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<BuildingOwner> Buildings { get; set; }

        
    }
}
