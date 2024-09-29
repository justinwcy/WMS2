using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Warehouse : EntityBase
    {
        public string Name { get; set; } = string.Empty;

        public string Address { get; set; }

        // one to many relationship
        [JsonIgnore]
        public Company Company { get; set; }
        public Guid CompanyId { get; set; }

        // one to many relationship
        public List<Zone> Zones { get; set; }
    }
}
