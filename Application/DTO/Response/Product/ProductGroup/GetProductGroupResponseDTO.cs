using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetProductGroupResponseDTO : ProductGroupBaseDTO
    {
        public Guid Id { get; set; }
    }
}