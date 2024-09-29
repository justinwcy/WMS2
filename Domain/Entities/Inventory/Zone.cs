using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Zone : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        // one to many relationship
        public Guid WarehouseId { get; set; }

        [JsonIgnore]
        public Warehouse Warehouse { get; set; }

        // many to many relationship
        [JsonIgnore]
        public List<Staff>? Staffs { get; set; }

        // one to many relationship
        public List<Rack> Racks { get; set; }
    }
}
