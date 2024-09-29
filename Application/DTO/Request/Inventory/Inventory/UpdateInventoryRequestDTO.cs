using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateInventoryRequestDTO : InventoryBaseDTO
    {
        public Guid Id { get; set; }
    }
}
