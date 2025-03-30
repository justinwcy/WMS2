using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Application.CsvConverter
{
    public class GuidListConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var guidStringArray = text.Split([','], StringSplitOptions.RemoveEmptyEntries);

            var result = guidStringArray.Select(s =>
            {
                if (Guid.TryParse(s.Trim(), out var parsedGuid))
                {
                    return parsedGuid;
                }
                else
                {
                    return Guid.Empty; // Or throw an exception, or handle the error as needed
                }
            }).ToList();

            return result;
        }
    }
}
