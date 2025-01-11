namespace WebUI.Components.Models
{
    public class WarehouseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Guid CompanyId { get; set; }
        public List<Guid> ZoneIds { get; set; }
    }
}
