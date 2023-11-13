namespace Task1_updated.Models
{
    public class FileContent
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
    }
}
