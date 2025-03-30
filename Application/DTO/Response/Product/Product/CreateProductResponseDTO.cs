using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateProductResponseDTO : ProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}
