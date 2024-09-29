using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Product : EntityBase
    {
        public List<ProductSku> ProductSkus { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public string Tag { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        // many to many relationship
        [JsonIgnore]
        public List<IncomingOrder> IncomingOrders { get; set; }

        // many to many relationship
        [JsonIgnore]
        public List<RefundOrder> RefundOrders { get; set; }

        // many to many relationship
        [JsonIgnore]
        public List<Shop> Shops { get; set; }

        // many to many relationship
        [JsonIgnore]
        public List<Rack> Racks { get; set; }

        // one to one relationship
        [JsonIgnore]
        public Inventory Inventory { get; set; }

        // one to many relationship
        [JsonIgnore]
        public List<Location> Locations { get; set; } 

        // one to many relationship
        [JsonIgnore]
        public List<CustomerOrderDetail> CustomerOrderDetails { get; set; }
    }
}
