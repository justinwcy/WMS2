namespace Application.DTO.BaseDTO
{
    public class IncomingOrderBaseDTO : BaseDTO
    {
        public DateTime IncomingOrderDate { get; set; }
        public DateTime ReceivingDate { get; set; }
        public string Status { get; set; }
        public Guid VendorId { get; set; }
        public string PONumber { get; set; }
    }
}
