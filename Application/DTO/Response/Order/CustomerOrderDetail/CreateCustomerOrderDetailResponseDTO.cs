using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateCustomerOrderDetailResponseDTO : CustomerOrderDetailBaseDTO
    {
        public Guid Id { get; set; }
    }
}
