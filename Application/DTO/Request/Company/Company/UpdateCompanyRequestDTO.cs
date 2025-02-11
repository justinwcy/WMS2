using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateCompanyRequestDTO : FileStorageBaseDTO
    {
        public Guid Id { get; set; }
    }
}
