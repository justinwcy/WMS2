using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateRackRequestDTO : RackBaseDTO
    {
        public Guid Id { get; set; }
    }
}
