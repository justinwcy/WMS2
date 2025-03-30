using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateRefundOrderResponseDTO : RefundOrderBaseDTO
    {
        public Guid Id { get; set; }
    }
}
