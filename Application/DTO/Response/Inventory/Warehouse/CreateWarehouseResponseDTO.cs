using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class CreateWarehouseResponseDTO : WarehouseBaseDTO
    {
        public Guid Id { get; set; }
    }
}
