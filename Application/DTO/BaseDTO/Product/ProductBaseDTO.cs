using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class ProductBaseDTO : BaseDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Tag { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public string Sku { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid> ProductGroupIds { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? ShopIds { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? IncomingOrderIds { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? RefundOrderIds { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? RackIds { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? CustomerOrderDetailIds { get; set; }
        
    }
}
