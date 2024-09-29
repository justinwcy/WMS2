using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateIncomingOrderRequestDTO : IncomingOrderBaseDTO
    {
        public Guid Id { get; set; }
    }
}
