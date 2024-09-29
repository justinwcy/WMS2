namespace Domain.Entities
{
    public class ProductLocation : EntityBase
    {
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
