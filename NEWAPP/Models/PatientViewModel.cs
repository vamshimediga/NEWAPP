using DomainModels;
using System.ComponentModel.DataAnnotations;

namespace NEWAPP.Models
{
    public class PatientViewModel
    {
        public int PatientID { get; set; }
       
        public string PatientName { get; set; }

    
        public int DoctorID { get; set; }

        public List<DoctorViewModel> Doctors { get; set; }
    }
}
