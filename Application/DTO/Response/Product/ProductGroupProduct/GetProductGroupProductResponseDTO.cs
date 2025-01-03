using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetProductGroupProductResponseDTO : ProductGroupProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}