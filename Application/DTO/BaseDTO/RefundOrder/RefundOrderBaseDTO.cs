namespace Application.DTO.BaseDTO
{
    public class RefundOrderBaseDTO : BaseDTO
    {
        public required string RefundReason { get; set; } = string.Empty;

        public required string Status { get; set; } = string.Empty;

        public DateTime ReceivingDate { get; set; }

        public List<Guid> RefundOrderProductIds { get; set; }
    }
}
