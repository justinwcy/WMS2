using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class ProductGroup : EntityBase
    {
        public string Name { get; set; }

        public string Description { get; set; }

        // many to many relationship
        [JsonIgnore]
        public List<Product>? Products { get; set; } = [];
    }
}
