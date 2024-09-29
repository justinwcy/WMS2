using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Courier : EntityBase
    {
        public required string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public required decimal Price { get; set; }

        public string Remark { get; set; }

        // one to many relationship
        [JsonIgnore]
        public List<CustomerOrder> CustomerOrders { get; set; }
    }
}
