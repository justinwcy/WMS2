using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateCustomerOrderResponseDTO : CustomerOrderBaseDTO
    {
        public Guid Id { get; set; }
    }
}
