namespace WebUI.Components.Models
{
    public class ShopModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }

        public string ProductIdsString { get; set; } = string.Empty;

        public List<Guid> ProductIds
        {
            get
            {
                if (ProductIdsString == string.Empty)
                {
                    return new List<Guid>();
                }

                return ProductIdsString
                    .Split(", ")
                    .Select(productId => new Guid(productId.Trim()))
                    .ToList();
            }
        }
    }
}
