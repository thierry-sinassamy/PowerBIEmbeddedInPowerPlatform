using System.Text.Json.Serialization;

namespace PowerBiEmbedder.PowerBiModel
{
    public class RootReport
    {
        [JsonPropertyName("@odata.context")]
        public string? OdataContext { get; set; }
        public List<Report>? value { get; set; }
    }
    public class Report
    {
        public string? id { get; set; }
        public string? name { get; set; }
        public string? reportType { get; set; }
        public string? webUrl { get; set; }
        public string? embedUrl { get; set; }
        public bool isFromPbix { get; set; }
        public bool isOwnedByMe { get; set; }
        public Guid datasetId { get; set; }
        public Guid datasetWorkspaceId { get; set; }
        public int reportFlags { get; set; }
    }
}
