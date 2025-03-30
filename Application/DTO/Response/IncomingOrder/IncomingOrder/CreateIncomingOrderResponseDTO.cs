using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateIncomingOrderResponseDTO : IncomingOrderBaseDTO
    {
        public Guid Id { get; set; }
    }
}
