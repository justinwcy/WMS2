using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateProductSkuRequestDTO : ProductSkuBaseDTO
    {
        public Guid Id { get; set; }
    }
}
