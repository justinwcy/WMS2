using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateShopRequestDTO : ShopBaseDTO
    {
        public Guid Id { get; set; }
    }
}
