namespace Domain.Entities
{
    public class IncomingOrder : EntityBase
    {
        public string PONumber { get; set; }

        public DateTime IncomingDate { get; set; }

        public DateTime ReceivingDate { get; set; }

        public string Status { get; set; } = string.Empty;

        // one to many relationship
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }

        public List<IncomingOrderProduct> IncomingOrderProducts { get; set; }
    }
}
