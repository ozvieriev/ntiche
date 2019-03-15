using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace Site.UI.Core.Csv
{
    internal class CsvDateTimeConverter : DefaultTypeConverter
    {
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value == null)
                return string.Empty;

            return ((DateTime)value).ToString("MM-dd-yyyy");
        }
    }
}