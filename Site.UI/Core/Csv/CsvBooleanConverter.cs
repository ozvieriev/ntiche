using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Site.UI.Core.Csv
{
    internal class CsvBooleanConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
                return string.Empty;

            return (bool)value ? "yes" : "no";
        }
    }
}