
using System.Text.Json.Serialization;

namespace PowerBiEmbedder.PowerBiModel
{
    public class RootDatasource
    {
        [JsonPropertyName("@odata.context")]
        public string? OdataContext { get; set; }
        public List<Datasource>? value { get; set; }
    }

    public class Datasource
    {
        public string? datasourceType { get; set; }
        public ConnectionDetail? connectionDetails { get; set; }
        public Guid datasourceId { get; set; }
        public Guid gatewayId { get; set; }
    }

    public class ConnectionDetail
    {        
        public string? server { get; set; }
        public string? systemNumber { get; set; } 
        public string? clientId { get; set; } 
    }
}
