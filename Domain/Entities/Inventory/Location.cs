namespace Domain.Entities
{
    public class Location : EntityBase
    {
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public Guid ZoneId { get; set; }
        public Zone Zone { get; set; }
        public Guid RackId { get; set; }
        public Rack Rack { get; set; }
        public List<Product> Products { get; set; }
    }
}
