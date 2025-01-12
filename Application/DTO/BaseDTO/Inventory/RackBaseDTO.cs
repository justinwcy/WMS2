using System.Text.Json.Serialization;

namespace Application.DTO.BaseDTO
{
    public class RackBaseDTO : BaseDTO
    {
        public Guid ZoneId { get; set; }

        public string Name { get; set; }

        public double MaxWeight { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Depth { get; set; }

        public List<Guid> ProductIds { get; set; }
    }
}
