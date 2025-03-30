using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateProductGroupResponseDTO : ProductGroupBaseDTO
    {
        public Guid Id { get; set; }
    }
}
