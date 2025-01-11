namespace WebUI.Components.Models
{
    public class CustomerOrderModel
    {
        public Guid Id { get; set; }
        public DateTime ExpectedArrivalDate { get; set; }
        public DateTime OrderCreationDate { get; set; }
        public string OrderAddress { get; set; }
        public Guid CustomerId { get; set; }
        public List<Guid> CustomerOrderDetailIds { get; set; }
        public Guid CourierId { get; set; }
        public Guid BinId { get; set; }
    }
}
