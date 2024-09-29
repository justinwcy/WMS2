namespace Domain.Entities
{
    public class IncomingOrder : EntityBase
    {
        public string PONumber { get; set; }

        public DateTime IncomingDate { get; set; }

        public DateTime ReceivingDate { get; set; }

        public string Status { get; set; } = string.Empty;

        // many to many relationship
        public List<Product> Products { get; set; }

        // one to many relationship
        public Guid VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
