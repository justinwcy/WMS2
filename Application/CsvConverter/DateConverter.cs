using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Application.CsvConverter
{
    public class DateConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return DateTime.Parse(text);
        }
    }
}
