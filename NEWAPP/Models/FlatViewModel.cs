using DomainModels;

namespace NEWAPP.Models
{
    public class FlatViewModel
    {
        public int FlatID { get; set; }
        public int PropertyOwnerID { get; set; }
        public string FlatNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; } // Male, Female, Other, etc.


        public bool IsActive { get; set; } = true;
        public PropertyOwnerViewModel PropertyOwner { get; set; }
        public List<PropertyOwnerViewModel> PropertyOwners { get; set; } 
    }
}
