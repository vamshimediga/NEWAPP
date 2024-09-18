using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class HumanResources
    {
        public int EmployeeID { get; set; }  // Corresponds to EmployeeID in the table
        public string FirstName { get; set; }  // Corresponds to FirstName in the table
        public string LastName { get; set; }  // Corresponds to LastName in the table
        public string Email { get; set; }  // Corresponds to Email in the table
        public DateTime HireDate { get; set; }  // Corresponds to HireDate in the table
    }
}
