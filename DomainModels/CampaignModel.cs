using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class CampaignModel
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Budget { get; set; }

        // Navigation Property
        public ICollection<MeetingModel> Meetings { get; set; }
    }
}
