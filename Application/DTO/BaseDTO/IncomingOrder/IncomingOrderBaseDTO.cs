using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class IncomingOrderBaseDTO : BaseDTO
    {
        [TypeConverter(typeof(DateConverter))]
        public DateTime IncomingDate { get; set; }

        [TypeConverter(typeof(DateConverter))]
        public DateTime ReceivingDate { get; set; }
        public string Status { get; set; }
        public Guid? VendorId { get; set; }
        public string PONumber { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? IncomingOrderProductIds { get; set; }
    }
}
