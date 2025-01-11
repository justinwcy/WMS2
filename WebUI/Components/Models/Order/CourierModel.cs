namespace WebUI.Components.Models
{
    public class CourierModel
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public string Remark { get; set; }
        public List<Guid> CustomerOrderIds { get; set; }
    }
}
