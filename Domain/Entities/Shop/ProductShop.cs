namespace Domain.Entities
{
    public class ProductShop : EntityBase
    {
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid? ShopId { get; set; }
        public Shop? Shop { get; set; }
    }
}
