using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class RefundOrderBaseDTO : BaseDTO
    {
        public string RefundReason { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        [TypeConverter(typeof(DateConverter))]
        public DateTime RefundDate { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? RefundOrderProductIds { get; set; }
    }
}
