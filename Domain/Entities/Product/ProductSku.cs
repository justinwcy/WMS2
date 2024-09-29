namespace Domain.Entities
{
    public class ProductSku : EntityBase
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public string Sku { get; set; }
    }
}
