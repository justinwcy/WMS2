using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetProductShopResponseDTO : ProductShopBaseDTO
    {
        public Guid Id { get; set; }
    }
}