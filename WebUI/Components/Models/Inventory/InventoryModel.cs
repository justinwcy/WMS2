namespace WebUI.Components.Models
{
    public class InventoryModel
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? RackId { get; set; }
        public int Quantity { get; set; }
    }
}
