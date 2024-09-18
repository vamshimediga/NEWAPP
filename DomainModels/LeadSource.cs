using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class LeadSource
    {
        public int LeadSourceID { get; set; } // Unique identifier for the lead source
        public string SourceName { get; set; } // Name of the lead source
        public string Description { get; set; } // Description of the lead source
        public DateTime CreatedDate { get; set; } // Timestamp when the record was created
        public DateTime ModifiedDate { get; set; } // Timestamp when the record was last modified
    }
}

