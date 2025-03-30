using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class ShopBaseDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Platform { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? ProductIds { get; set; }
    }
}
