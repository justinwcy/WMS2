namespace WebUI.Components.Models
{
    public class RefundOrderModel
    {
        public Guid Id { get; set; }
        public required string RefundReason { get; set; } = string.Empty;
        public required string Status { get; set; } = string.Empty;
        public DateTime ReceivingDate { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}
