using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetRackResponseDTO : RackBaseDTO
    {
        public Guid Id { get; set; }
    }
}