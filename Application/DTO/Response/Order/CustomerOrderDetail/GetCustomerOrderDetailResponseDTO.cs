using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetCustomerOrderDetailResponseDTO : CustomerOrderDetailBaseDTO
    {
        public Guid Id { get; set; }
    }
}