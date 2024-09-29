using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateLocationRequestDTO : LocationBaseDTO
    {
        public Guid Id { get; set; }
    }
}
