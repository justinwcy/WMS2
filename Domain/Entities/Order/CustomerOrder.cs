using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class CustomerOrder : EntityBase
    {
        public DateTime ExpectedArrivalDate { get; set; }

        public DateTime OrderCreationDate { get; set; }

        public string OrderAddress { get; set; }

        // one to many relationship
        public Guid CustomerId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }

        // one to many relationship
        public List<CustomerOrderDetail> CustomerOrderDetails { get; set; }

        // one to many relationship
        public Courier Courier { get; set; }
        public Guid CourierId { get; set; }

        public Bin Bin { get; set; }
        public Guid BinId { get; set; }
    }
}
