using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateProductShopRequestDTO : ProductShopBaseDTO
    {
        public Guid Id { get; set; }
    }
}
