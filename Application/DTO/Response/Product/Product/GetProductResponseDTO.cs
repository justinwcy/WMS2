using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetProductResponseDTO : ProductBaseDTO
    {
        public Guid Id { get; set; }
    }
}