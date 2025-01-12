namespace WebUI.Components.Models
{
    public class ZoneModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid WarehouseId { get; set; }
        public List<Guid> StaffIds { get; set; }
        public List<Guid> RackIds { get; set; }
    }
}
