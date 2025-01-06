namespace WebUI.Components.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public List<Guid> ProductGroupIds { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Tag { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public string Sku { get; set; }
    }
}
