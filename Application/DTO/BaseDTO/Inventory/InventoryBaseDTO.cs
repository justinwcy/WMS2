namespace Application.DTO.BaseDTO
{
    public class InventoryBaseDTO : BaseDTO
    {
        public Guid? ProductId { get; set; }
        public int Quantity { get; set; }
        public int DaysLeadTime { get; set; }
    }
}
