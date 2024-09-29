using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetIncomingOrderResponseDTO : IncomingOrderBaseDTO
    {
        public Guid Id { get; set; }
    }
}
