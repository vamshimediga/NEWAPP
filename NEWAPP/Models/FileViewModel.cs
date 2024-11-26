namespace NEWAPP.Models
{
    public class FileViewModel
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string FileType { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsArchived { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string Notes { get; set; }
        public string Tags { get; set; }
    }

}
