namespace IISTestApplication.Models
{
    public class FileMetadata
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public int? FileId { get; set; }

        public File File { get; set; }
    }
}
