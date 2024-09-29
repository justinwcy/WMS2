using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateRefundOrderProductRequestDTO : RefundOrderProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}
