using DomainModels;

namespace NEWAPP.Models
{
    public class PersonDataViewModel
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public AddressDataViewModel Address { get; set; } // One-to-One relationship
    }
}
