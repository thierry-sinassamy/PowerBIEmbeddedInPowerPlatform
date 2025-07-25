using Newtonsoft.Json;

namespace PowerBiEmbedder.PowerBiObject
{
    public class Target
    {
        [JsonProperty("table")]
        public string? Table { get; set; }

        [JsonProperty("column")]
        public string? Column { get; set; }
    }
}
