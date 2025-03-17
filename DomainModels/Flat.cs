using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Flat
    {
        public int FlatID { get; set; }
        public int PropertyOwnerID { get; set; }
        public string FlatNumber { get; set; }
        public string Address { get; set; }
        
        public string Gender { get; set; } // Male, Female, Other, etc.

       
        public bool IsActive { get; set; } = true;

        public PropertyOwner PropertyOwner { get; set; }
        public List<PropertyOwner> PropertyOwners { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
    }
}
