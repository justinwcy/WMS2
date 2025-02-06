namespace WebUI.Components.Models
{
    public class RefundOrderProductModel
    {
        public Guid Id { get; set; }
        public Guid? RefundOrderId { get; set; }
        public Guid? ProductId { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
    }
}
