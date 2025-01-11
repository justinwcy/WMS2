namespace WebUI.Components.Models
{
    public class IncomingOrderProductModel
    {
        public Guid Id { get; set; }
        public Guid IncomingOrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
    }
}
