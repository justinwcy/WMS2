namespace WebUI.Components.Models
{
    public class RackModel
    {
        public Guid Id { get; set; }
        public Guid ZoneId { get; set; }
        public List<Guid> ProductIds { get; set; } = [];
        public string Name { get; set; }
        public double MaxWeight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
    }
}
