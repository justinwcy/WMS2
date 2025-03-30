using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Application.CsvConverter
{
    public class StringStringDictionaryConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            var dictionaryStringArray = text.Split([','], StringSplitOptions.RemoveEmptyEntries);

            var dictionaryOutput = new Dictionary<string, string>();
            foreach (var dictionaryString in dictionaryStringArray)
            {
                var keyValue = dictionaryString.Split(':');

                if (keyValue.Length == 2)
                {
                    var key = keyValue[0].Trim();
                    var value = keyValue[1].Trim();

                    dictionaryOutput[key] = value;
                }
            }

            return dictionaryOutput;
        }
    }
}
