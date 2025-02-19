namespace NEWAPP.Models
{
    public class ConstructionBuilderViewModel
    {
        public int BuilderID { get; set; }
        public string BuilderName { get; set; }
        public string CompanyName { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public List<BuildingOwnerViewModel> Buildings { get; set; }
    }
}
