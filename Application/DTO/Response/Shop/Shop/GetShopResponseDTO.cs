using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetShopResponseDTO : ShopBaseDTO
    {
        public Guid Id { get; set; }
    }
}