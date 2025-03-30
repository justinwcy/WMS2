using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class CustomerOrderBaseDTO : BaseDTO
    {
        public DateTime ExpectedArrivalDate { get; set; }

        public DateTime OrderCreationDate { get; set; }

        public string OrderAddress { get; set; }

        public Guid? CustomerId { get; set; }
        public Guid? CourierId { get; set; }
        public Guid? BinId { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? CustomerOrderDetailIds { get; set; }
    }
}
