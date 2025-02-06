using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class ProductGroupProduct : EntityBase
    {
        public Guid? ProductGroupId { get; set; }
        public ProductGroup? ProductGroup { get; set; }

        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
