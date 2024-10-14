namespace NEWAPP.Models
{
    public class AddressDataViewModel
    {
        public int AddressID { get; set; }
        public int PersonID { get; set; }  // Foreign Key
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
    }
}
