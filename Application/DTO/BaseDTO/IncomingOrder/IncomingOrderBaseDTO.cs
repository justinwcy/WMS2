namespace Application.DTO.BaseDTO
{
    public class IncomingOrderBaseDTO : BaseDTO
    {
        public DateTime IncomingDate { get; set; }
        public DateTime RefundDate { get; set; }
        public string Status { get; set; }
        public Guid VendorId { get; set; }
        public string PONumber { get; set; }
        public List<Guid> IncomingOrderProductIds { get; set; }
    }
}
