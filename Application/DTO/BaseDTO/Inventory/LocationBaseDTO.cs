namespace Application.DTO.BaseDTO
{
    public class LocationBaseDTO : BaseDTO
    {
        public Guid WarehouseId { get; set; }
        public Guid ZoneId { get; set; }
        public Guid RackId { get; set; }
    }
}
