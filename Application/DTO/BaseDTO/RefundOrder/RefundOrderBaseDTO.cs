namespace Application.DTO.BaseDTO
{
    public class RefundOrderBaseDTO : BaseDTO
    {
        public string RefundReason { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateTime RefundDate { get; set; }

        public List<Guid>? RefundOrderProductIds { get; set; }
    }
}
