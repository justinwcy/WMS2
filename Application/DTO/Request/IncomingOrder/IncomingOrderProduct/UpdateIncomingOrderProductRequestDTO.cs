using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateIncomingOrderProductRequestDTO : IncomingOrderProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}
