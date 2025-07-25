using Newtonsoft.Json;

namespace PowerBiEmbedder.PowerBiObject
{
    public class Filter
    {
        [JsonProperty("$schema")]
        public string? Schema { get; set; }

        [JsonProperty("target")]
        public Target? Target { get; set; }

        [JsonProperty("operator")]
        public string? Operator { get; set; }

        [JsonProperty("values")]
        public string[]? Values { get; set; }

        [JsonProperty("filterType")]
        public int FilterType { get; set; }
    }
}
