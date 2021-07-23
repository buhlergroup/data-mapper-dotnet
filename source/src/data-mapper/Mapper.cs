namespace Buhler.DataMapper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Buhler.DataMapper.Condition;
    using Buhler.DataMapper.Helper;
    using Buhler.DataMapper.Model;
    using Buhler.DataMapper.Operation;
    using Buhler.DataMapper.Validation;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Mapper : IMapper
    {
        private const string MappingFileName = "field-mapping.json";
        private readonly IStreamHelper _streamHelper;
        private readonly IFieldValidation _validationHelper;

        public Mapper(IStreamHelper streamHelper, IFieldValidation validationHelper)
        {
            _streamHelper = streamHelper;
            _validationHelper = validationHelper;
        }

        public object GetFieldValue(JObject model, FieldMappingModel map)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (map.Disabled)
            {
                return null;
            }

            var result = ConditionCheck.ApplyFristMatchingCondition(map.Conditions, model);
            if (!String.IsNullOrEmpty(result))
            {
                return result;
            }

            if (map.Combination != FieldCombination.UNKNOWN)
            {
                var fieldOperation = OperationFactory.GetFieldOperation(map.Combination);
                return fieldOperation.Execute(model, map, _validationHelper);
            }

            if (map.SourceFields?.Any() != true)
            {
                return null;
            }

            var value = model.Value<object>(map.SourceFields.First());
            if (!_validationHelper.ValidateField(map, value))
            {
                return null;
            }

            return map.Type switch
            {
                FieldType.SELECT => map.SelectValues.Where(x => x.Origin == value.ToString()).Select(x => x.Destination).First(),
                FieldType.NUMBER => float.Parse(value.ToString()),
                _ => value.ToString(),
            };
        }

        public Dictionary<string, object> MapToDictionary(JObject model, IEnumerable<FieldMappingModel> mapping)
        {
            var dict = new Dictionary<string, object>();
            foreach (var map in mapping)
            {
                var fieldValue = GetFieldValue(model, map);
                if (fieldValue != null)
                {
                    dict.Add(map.TargetField, fieldValue);
                }
            }

            return dict;
        }

        public async Task<IEnumerable<FieldMappingModel>> GetFieldMappingsAsync(string mappingDirectory)
        {
            if (String.IsNullOrEmpty(mappingDirectory))
            {
                mappingDirectory = Directory.GetCurrentDirectory();
            }

            var mappingFileContent = await _streamHelper.FileToStringAsync(Path.Join(mappingDirectory, MappingFileName)).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<FieldMappingModel>>(mappingFileContent);
        }
    }
}
