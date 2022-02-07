namespace Buhlergroup.DataMapper.Validation
{
    using System;
    using System.Linq;
    using Buhlergroup.DataMapper.Formatter;
    using Buhlergroup.DataMapper.Model;

    public class FieldValidation : IFieldValidation
    {
        public bool ValidateField(FieldMappingModel mapping, object field)
        {
            if (mapping == null)
            {
                throw new ArgumentNullException(nameof(mapping));
            }

            if (field == null)
            {
                return false;
            }

            return mapping.Type switch
            {
                FieldType.DATE => DateTimeFormatter.CanFormat(mapping, field),
                FieldType.NUMBER => float.TryParse(field.ToString(), out _),
                FieldType.TEXT => true,
                FieldType.SELECT => ValidateSelect(mapping, field),
                _ => true,
            };
        }

        private static bool ValidateSelect(FieldMappingModel mapping, object field)
        {
            return mapping.SelectValues?.Any(x => x.Origin.Equals(field.ToString(), StringComparison.Ordinal)) ?? false;
        }
    }
}
