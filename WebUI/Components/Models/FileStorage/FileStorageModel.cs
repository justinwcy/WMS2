namespace WebUI.Components.Models
{
    public class FileStorageModel
    {
        public Guid Id { get; set; }
        public string FileLink { get; set; }
        public string StorageType { get; set; }
        public Stream FileStream { get; set; }
    }
}
