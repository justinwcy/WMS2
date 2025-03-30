using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateRackResponseDTO : RackBaseDTO
    {
        public Guid Id { get; set; }
    }
}
