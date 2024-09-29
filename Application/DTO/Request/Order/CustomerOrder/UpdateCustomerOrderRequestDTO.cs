using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateCustomerOrderRequestDTO : CustomerOrderBaseDTO
    {
        public Guid Id { get; set; }
    }
}
