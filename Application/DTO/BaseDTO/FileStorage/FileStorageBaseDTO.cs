namespace Application.DTO.BaseDTO
{
    public class FileStorageBaseDTO : BaseDTO
    {
        public Stream FileStream { get; set; }
        public string FileLink { get; set; }
        public string StorageType { get; set; }
    }
}
