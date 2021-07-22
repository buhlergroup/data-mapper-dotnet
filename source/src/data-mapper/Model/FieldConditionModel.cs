namespace Buhler.DataMapper.Model
{
    using Newtonsoft.Json;

    public class FieldConditionModel
    {
        [JsonProperty("type")]
        public ConditionType Type { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("equals")]
        public string EqualsValue { get; set; }
    }
}
