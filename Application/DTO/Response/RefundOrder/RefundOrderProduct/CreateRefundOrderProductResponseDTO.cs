using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateRefundOrderProductResponseDTO : RefundOrderProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}
