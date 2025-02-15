using DomainModels;

namespace NEWAPP.Models
{
    public class CampaignViewModel
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }

        public string MeetingID { get; set; }
        // Navigation Property
        public ICollection<MeetingViewModel> Meetings { get; set; }
    }
}
