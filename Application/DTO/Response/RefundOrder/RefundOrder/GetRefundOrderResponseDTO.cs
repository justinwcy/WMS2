using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetRefundOrderResponseDTO : RefundOrderBaseDTO
    {
        public Guid Id { get; set; }
    }
}