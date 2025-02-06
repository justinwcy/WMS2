namespace Domain.Entities
{
    public class ProductRack : EntityBase
    {
        public Guid? RackId { get; set; }
        public Rack? Rack { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
