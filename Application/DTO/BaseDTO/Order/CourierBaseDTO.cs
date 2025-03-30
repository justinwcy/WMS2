using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class CourierBaseDTO : BaseDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Remark { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? CustomerOrderIds { get; set; }
    }
}
