using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateCustomerResponseDTO : CustomerBaseDTO
    {
        public Guid Id { get; set; }
    }
}
