namespace Buhlergroup.DataMapper.Model
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class FieldMappingModel
    {
        [JsonProperty("target-field")]
        public string TargetField { get; set; }

        [JsonProperty("source-fields")]
        public IEnumerable<string> SourceFields { get; set; }

        [JsonProperty("field-combination")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldCombination Combination { get; set; }

        [JsonProperty("conditions")]
        public IEnumerable<FieldConditionModel> Conditions { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FieldType Type { get; set; }

        [JsonProperty("in-format")]
        public string InFormat { get; set; }

        [JsonProperty("out-format")]
        public string OutFormat { get; set; }

        [JsonProperty("select-values")]
        public IEnumerable<SelectFieldMappingModel> SelectValues { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }
    }
}
