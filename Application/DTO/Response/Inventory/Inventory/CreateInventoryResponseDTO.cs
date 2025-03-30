using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateInventoryResponseDTO : InventoryBaseDTO
    {
        public Guid Id { get; set; }
    }
}
