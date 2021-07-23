namespace Buhler.DataMapper.Operation
{
    using System;
    using Buhler.DataMapper.Model;
    using Buhler.DataMapper.Validation;
    using Newtonsoft.Json.Linq;

    public class OrOperation : IOperation
    {
        public string Execute(JObject model, FieldMappingModel map, IFieldValidation validationHelper)
        {
            ValidateArguments(model, map, validationHelper);

            foreach (var field in map.SourceFields)
            {
                var value = model.Value<object>(field);
                if (value != null && validationHelper.ValidateField(map, value))
                {
                    return value.ToString();
                }
            }

            return null;
        }

        private static void ValidateArguments(JObject model, FieldMappingModel map, IFieldValidation validationHelper)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (validationHelper == null)
            {
                throw new ArgumentNullException(nameof(validationHelper));
            }
        }
    }
}
