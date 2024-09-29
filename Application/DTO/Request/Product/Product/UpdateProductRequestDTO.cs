using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateProductRequestDTO : ProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}
