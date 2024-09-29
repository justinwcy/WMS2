using Application.DTO.BaseDTO;

namespace Application.DTO.Response
{
    public class GetWarehouseResponseDTO : WarehouseBaseDTO
    {
        public Guid Id { get; set; }
    }
}