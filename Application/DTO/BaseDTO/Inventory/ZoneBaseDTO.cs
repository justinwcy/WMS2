using System.Text.Json.Serialization;

namespace Application.DTO.BaseDTO
{
    public class ZoneBaseDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public Guid WarehouseId { get; set; }
        public List<Guid> StaffIds { get; set; }
    }
}
