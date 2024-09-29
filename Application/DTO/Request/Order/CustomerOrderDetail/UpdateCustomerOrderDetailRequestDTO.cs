using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateCustomerOrderDetailRequestDTO : CustomerOrderDetailBaseDTO
    {
        public Guid Id { get; set; }
    }
}
