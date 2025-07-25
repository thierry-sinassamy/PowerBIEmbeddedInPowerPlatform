using System.Text.Json.Serialization;

namespace PowerBiEmbedder.PowerBiModel
{
    public class RootDataset
    {
        [JsonPropertyName("@odata.context")]
        public string? OdataContext { get; set; }
        public List<Dataset>? value { get; set; }
    }
    
    public class Dataset
    {
        public Guid id { get; set; }
        public string? webUrl { get; set; }
        public bool addRowsAPIEnabled { get; set; }
        public string? configuredBy { get; set; }
        public bool isRefreshable { get; set; }
        public bool isEffectiveIdentityRequired { get; set; }
        public bool isEffectiveIdentityRolesRequired { get; set; }
        public bool isOnPremGatewayRequired { get; set; }
        public string? targetStorageMode { get; set; }
        public DateTimeOffset createdDate { get; set; }
        public string? createReportEmbedURL { get; set; }
        public string[]? upstreamDatasets { get; set; }
        public string[]? users { get; set; }
        public Dictionary<string, QueryScaleOutSettings>? queryScaleOutSettings { get; set; }
    }

    public class QueryScaleOutSettings
    {
        public bool autoSyncReadOnlyReplicas { get; set; }
        public int maxReadOnlyReplicas { get; set; }
        
    }
}

