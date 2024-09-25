namespace NEWAPP.Models
{
    public class HumanResourcesViewModel
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string HireDate { get; set; } // Consider transforming DateTime to string for easier representation
    }
}
