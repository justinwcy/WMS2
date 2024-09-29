using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateRefundOrderRequestDTO : RefundOrderBaseDTO
    {
        public Guid Id { get; set; }
    }
}
