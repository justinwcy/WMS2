using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateProductShopResponseDTO : ProductShopBaseDTO
    {
        public Guid Id { get; set; }
    }
}
