using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class WarehouseBaseDTO : BaseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; }
        public Guid? CompanyId { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? ZoneIds { get; set; }
    }
}
