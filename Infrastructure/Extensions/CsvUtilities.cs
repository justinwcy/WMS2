using System.Globalization;

using CsvHelper.Configuration;

namespace Infrastructure.Extensions
{
    internal static class CsvUtilities
    {
        public static CsvConfiguration GetCsvConfiguration()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null, // Disable header validation
                MissingFieldFound = null // disable missing field validation if needed.
            };

            return config;
        }
    }
}
