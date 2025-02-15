using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class MeetingModel
    {
        public int MeetingID { get; set; }
        public int CampaignID { get; set; }
        public DateTime MeetingDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        // Navigation Property
        public ICollection<CampaignModel> Campaigns { get; set; }
    }
}
