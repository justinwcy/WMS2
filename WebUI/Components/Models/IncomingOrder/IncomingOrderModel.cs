namespace WebUI.Components.Models
{
    public class IncomingOrderModel
    {
        public Guid Id { get; set; }

        public string PONumber { get; set; }

        public DateTime IncomingDate { get; set; }

        public DateTime ReceivingDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public List<Guid> ProductIds { get; set; }

        public Guid VendorId { get; set; }
    }
}
