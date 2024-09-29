using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetCustomerOrderResponseDTO : CustomerOrderBaseDTO
    {
        public Guid Id { get; set; }
    }
}