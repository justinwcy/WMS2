namespace Application.DTO.BaseDTO
{
    public class IncomingOrderProductBaseDTO : BaseDTO
    {
        public Guid IncomingOrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
    }
}
