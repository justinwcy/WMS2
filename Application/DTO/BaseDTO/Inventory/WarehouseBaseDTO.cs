namespace Application.DTO.BaseDTO
{
    public class WarehouseBaseDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; }
        public Guid CompanyId { get; set; }
    }
}
