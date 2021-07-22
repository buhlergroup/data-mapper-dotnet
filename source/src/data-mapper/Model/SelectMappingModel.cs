namespace Buhler.DataMapper.Model
{
    using Newtonsoft.Json;

    public class SelectFieldMappingModel
    {
        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }
    }
}
