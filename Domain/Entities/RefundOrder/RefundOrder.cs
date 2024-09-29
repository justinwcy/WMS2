namespace Domain.Entities
{
    public class RefundOrder : EntityBase
    {
        public required string RefundReason { get; set; } = string.Empty;

        public required string Status { get; set; } = string.Empty;

        public DateTime ReceivingDate { get; set; }
    }
}
