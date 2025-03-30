using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateProductGroupProductResponseDTO : ProductGroupProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}
