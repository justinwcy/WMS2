using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateProductGroupProductRequestDTO : ProductGroupProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}
