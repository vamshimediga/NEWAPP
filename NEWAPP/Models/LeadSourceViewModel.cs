namespace NEWAPP.Models
{
    public class LeadSourceViewModel
    {
        public int LeadSourceID { get; set; }
        public string LeadSourceName { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }
        // Additional fields for display purposes (optional)
        public string CreatedDateFormatted { get; set; }
    }
}
