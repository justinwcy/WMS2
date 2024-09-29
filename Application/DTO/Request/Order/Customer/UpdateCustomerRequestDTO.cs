using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateCustomerRequestDTO : CustomerBaseDTO
    {
        public Guid Id { get; set; }
    }
}
