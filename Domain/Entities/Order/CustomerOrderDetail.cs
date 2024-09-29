using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class CustomerOrderDetail : EntityBase
    {
        public Guid CustomerOrderId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }

        [JsonIgnore]
        public CustomerOrder CustomerOrder { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
