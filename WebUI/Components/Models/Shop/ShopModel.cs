namespace WebUI.Components.Models
{
    public class ShopModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }

        public List<Guid> ProductIds { get; set; } = [];
    }
}
