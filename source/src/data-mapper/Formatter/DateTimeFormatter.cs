namespace Buhlergroup.DataMapper.Formatter
{
    using System;
    using System.Globalization;
    using Buhlergroup.DataMapper.Model;

    public static class DateTimeFormatter
    {

        public static string Format(FieldMappingModel mapping, object value)
        {
            if (String.IsNullOrEmpty(mapping.InFormat))
            {
                return value.ToString();
            }

            return DateTime.ParseExact(value.ToString(), mapping.InFormat, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString(mapping.OutFormat);
        }

        public static bool CanFormat(FieldMappingModel mapping, object value)
        {
            if (String.IsNullOrEmpty(mapping.InFormat))
            {
                return DateTime.TryParse(value.ToString(), out _);
            }

            return DateTime.TryParseExact(value.ToString(), mapping.InFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }

}
