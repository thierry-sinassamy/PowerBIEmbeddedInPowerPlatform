
using System.Text.Json.Serialization;

namespace PowerBiEmbedder.PowerBiModel
{
    public class RootGroup
    {
        [JsonPropertyName("@odata.context")]
        public string? OdataContext { get; set; }
        public List<Group>? value { get; set; }
    }
    public class Group
    {
        public bool isReadOnly { get; set; }
        public bool isOnDedicatedCapacity { get; set; }
        public Guid capacityId { get; set; }
        public string? defaultDatasetStorageFormat { get; set; }
        public string? type { get; set; }
        public Guid id { get; set; }
        public string? name { get; set; }
    }
}
