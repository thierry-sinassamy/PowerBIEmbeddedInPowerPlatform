using Newtonsoft.Json;

namespace PowerBiEmbedder.PowerBiObject
{
    public class Alias
    {
        [JsonProperty("$a")]
        public string? A { get; set; }
    }
}
