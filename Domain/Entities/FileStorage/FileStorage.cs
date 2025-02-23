namespace Domain.Entities
{
    public class FileStorage : EntityBase
    {
        public string FileLink { get; set; }

        public string StorageType { get; set; }

        public Guid? ProductGroupId { get; set; }

        public ProductGroup? ProductGroup { get; set; }
    }
}
