using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class BinBaseDTO : BaseDTO
    {
        public string Name { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? CustomerOrderIds { get; set; }
    }
}
