using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public int DoctorID { get; set; } // Foreign key to Doctor

        // Navigation property
        public List<Doctor> Doctors { get; set; }
    }
}
