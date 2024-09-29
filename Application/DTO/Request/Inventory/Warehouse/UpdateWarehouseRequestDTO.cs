using Application.DTO.BaseDTO;

namespace Application.DTO.Request
{
    public class UpdateWarehouseRequestDTO : WarehouseBaseDTO
    {
        public Guid Id { get; set; }
    }
}
