using System.Text.Json.Serialization;

using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class RackBaseDTO : BaseDTO
    {
        public Guid? ZoneId { get; set; }

        public string Name { get; set; }

        public double MaxWeight { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public double Depth { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? ProductIds { get; set; }
    }
}
