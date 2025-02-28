namespace Domain.Entities
{
    public class Inventory : EntityBase
    {
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid? RackId { get; set; }
        public Rack? Rack { get; set; }
        public int Quantity { get; set; }
    }
}
