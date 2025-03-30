using Application.CsvConverter;

using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class CompanyBaseDTO : BaseDTO
    {
        public string Name { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? StaffIds { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? WarehouseIds { get; set; }
    }
}
