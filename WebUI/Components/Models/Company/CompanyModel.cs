namespace WebUI.Components.Models
{
    public class CompanyModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Guid>? StaffIds { get; set; } = [];

        public List<Guid>? WarehouseIds { get; set; } = [];

    }
}
