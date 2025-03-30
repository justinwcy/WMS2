using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class VendorBaseDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? IncomingOrderIds { get; set; }
    }
}
