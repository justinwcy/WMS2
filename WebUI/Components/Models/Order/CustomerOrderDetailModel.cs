namespace WebUI.Components.Models
{
    public class CustomerOrderDetailModel
    {
        public Guid Id { get; set; }
        public Guid CustomerOrderId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public Guid ProductId { get; set; }
    }
}
