using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Author_US
    {
        public int AuthorID { get; set; } // Primary Key
        public string AuthorName { get; set; } // NOT NULL
        public DateTime? BirthDate { get; set; } // Nullable
        public DateTime CreatedDate { get; set; } // Default to current date
    }
}
