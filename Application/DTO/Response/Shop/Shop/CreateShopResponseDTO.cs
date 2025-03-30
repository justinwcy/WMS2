using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateShopResponseDTO : ShopBaseDTO
    {
        public Guid Id { get; set; }
    }
}
