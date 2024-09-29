namespace Application.DTO.BaseDTO
{
    public class CustomerOrderDetailBaseDTO : BaseDTO
    {
        public Guid CustomerOrderId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public Guid ProductId { get; set; }
    }
}
