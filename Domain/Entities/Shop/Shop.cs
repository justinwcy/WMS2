namespace Domain.Entities
{
    public class Shop : EntityBase
    {
        public required string Name { get; set; }

        public required string Platform { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        // many to many relationship
        public List<Product>? Products { get; set; } = [];
    }
}
