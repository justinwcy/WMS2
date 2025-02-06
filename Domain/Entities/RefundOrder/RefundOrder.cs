namespace Domain.Entities
{
    public class RefundOrder : EntityBase
    {
        public required string RefundReason { get; set; } = string.Empty;

        public required string Status { get; set; } = string.Empty;

        public DateTime RefundDate { get; set; }

        // many to many relationship
        public List<RefundOrderProduct>? RefundOrderProducts { get; set; } = [];
    }
}
