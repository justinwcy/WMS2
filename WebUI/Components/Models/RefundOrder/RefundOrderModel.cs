namespace WebUI.Components.Models
{
    public class RefundOrderModel
    {
        public Guid Id { get; set; }
        public string RefundReason { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime? RefundDate { get; set; }
        public List<Guid>? RefundOrderProductIds { get; set; } = [];
    }
}
