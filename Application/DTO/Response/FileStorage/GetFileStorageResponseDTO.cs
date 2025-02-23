using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetFileStorageResponseDTO : FileStorageBaseDTO
    {
        public Guid Id { get; set; }
    }
}
