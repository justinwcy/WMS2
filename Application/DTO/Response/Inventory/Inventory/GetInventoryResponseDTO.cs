using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetInventoryResponseDTO : InventoryBaseDTO
    {
        public Guid Id { get; set; }
    }
}