using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class SampleStudent
    {
        // Represents the StudentID column in the database
        public int StudentID { get; set; }

        // Represents the FirstName column in the database
        public string FirstName { get; set; }

        // Represents the LastName column in the database
        public string LastName { get; set; }

        // Represents the Email column in the database
        public string Email { get; set; }

        // Represents the EnrollmentDate column in the database
        public DateTime EnrollmentDate { get; set; }

        // Optional: You can add a constructor if needed
       
    }
}
