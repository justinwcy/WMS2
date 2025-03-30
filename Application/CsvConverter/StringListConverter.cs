using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Application.CsvConverter
{
    public class StringListConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var stringArray = text.Split([','], StringSplitOptions.RemoveEmptyEntries);
            return stringArray.ToList();
        }
    }
}
