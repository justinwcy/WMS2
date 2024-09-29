using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetProductSkuResponseDTO : ProductSkuBaseDTO
    {
        public Guid Id { get; set; }
    }
}