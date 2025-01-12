namespace WebUI.Components.Models
{
    public class CourierModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Remark { get; set; }
        public List<Guid> CustomerOrderIds { get; set; } = [];
    }
}
