using Application.CsvConverter;
using CsvHelper.Configuration.Attributes;

namespace Application.DTO.BaseDTO
{
    public class StaffBaseDTO : BaseDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }

        [TypeConverter(typeof(StringListConverter))]
        public List<string> Roles { get; set; }

        [TypeConverter(typeof(StringStringDictionaryConverter))]
        public Dictionary<string, string> Claims { get; set; }

        public Guid? CompanyId { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? ZoneIds { get; set; }

        [TypeConverter(typeof(GuidListConverter))]
        public List<Guid>? StaffNotificationIds { get; set; }
    }
}
