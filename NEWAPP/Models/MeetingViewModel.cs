using DomainModels;

namespace NEWAPP.Models
{
    public class MeetingViewModel
    {

        public int MeetingID { get; set; }
        public int CampaignID { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        // Navigation Property
        public ICollection<CampaignViewModel> Campaigns { get; set; }
    }
}
