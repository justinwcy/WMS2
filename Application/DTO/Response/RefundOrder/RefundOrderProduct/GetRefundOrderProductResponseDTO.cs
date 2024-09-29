using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetRefundOrderProductResponseDTO : RefundOrderProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}