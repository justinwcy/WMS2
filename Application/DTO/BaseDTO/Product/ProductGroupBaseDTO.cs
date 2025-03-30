using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;
using Domain.Entities;

namespace Application.DTO.BaseDTO
{
    public class ProductGroupBaseDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? ProductIds { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? PhotoIds { get; set; }
    }
}
