using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetCustomerResponseDTO : CustomerBaseDTO
    {
        public Guid Id { get; set; }
    }
}