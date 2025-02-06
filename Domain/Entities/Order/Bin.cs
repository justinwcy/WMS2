using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Bin : EntityBase
    {
        public string Name { get; set; }

        // one to many relationship
        [JsonIgnore]
        public List<CustomerOrder>? CustomerOrders { get; set; } = [];
    }
}
