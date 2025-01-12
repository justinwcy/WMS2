namespace WebUI.Components.Models
{
    public class BinModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> CustomerOrderIds { get; set; } = [];
    }
}
