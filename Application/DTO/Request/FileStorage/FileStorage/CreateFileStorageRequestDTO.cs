using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class CreateFileStorageRequestDTO : FileStorageBaseDTO
    {
        public string FileName { get; set; }
    }
}
