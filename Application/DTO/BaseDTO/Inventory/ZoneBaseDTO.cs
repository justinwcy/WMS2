using System.Text.Json.Serialization;

using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class ZoneBaseDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public Guid? WarehouseId { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? StaffIds { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? RackIds { get; set; }
    }
}
