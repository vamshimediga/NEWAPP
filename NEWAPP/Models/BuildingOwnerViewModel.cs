using DomainModels;

namespace NEWAPP.Models
{
    public class BuildingOwnerViewModel
    {
        public int OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string PropertyAddress { get; set; }
        public int BuilderID { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<ConstructionBuilderViewModel> ConstructionBuilders { get; set; }
    }
}
