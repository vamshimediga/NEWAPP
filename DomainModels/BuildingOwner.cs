using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class BuildingOwner
    {
        public int OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string PropertyAddress { get; set; }
        public int BuilderID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<ConstructionBuilder> ConstructionBuilders { get; set; }=new List<ConstructionBuilder>();
    }
}
