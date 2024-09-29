namespace Domain.Entities
{
    public class RefundOrderProduct : EntityBase
    {
        public Guid RefundOrderId { get; set; }

        public Guid ProductId { get; set; }

        public string Status { get; set; }

        public int Quantity { get; set; }
    }
}
