
using System.Text.Json.Serialization;

namespace PowerBiEmbedder.PowerBiModel
{
    public class RootImport
    {
        [JsonPropertyName("@odata.context")]
        public string? OdataContext { get; set; }
        public List<Import>? value { get; set; }
    }

    public class Import
    {
        public Guid id { get; set; }
        public string? importState { get; set; }
        public DateTimeOffset createdDateTime { get; set; }
        public DateTimeOffset updatedDateTime { get; set; }
        public string? name { get; set; }
        public string? connectionType { get; set; }
        public string? source { get; set; }
        public IList<Datasets>? datasets { get; set; }
        public IList<Reports>? reports { get; set; }
    }

    public class Datasets
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? webUrl { get; set;}
        public string? settingsUrl { get; set; }

    }

    public class Reports
    {
        public Guid id { get; set; }
        public string? reportType { get; set; }
        public string? name { get; set; }
        public string? webUrl { get; set;}
        public string? embedUrl { get; set;}
        public bool isPbixLiveConnect { get; set; }
    }
}

/*
 id":"511299c1-e5f1-487a-938e-f25fdc83e680",
	  "importState":"Succeeded",
	  "createdDateTime":"2024-11-22T19:07:07.063Z",
	  "updatedDateTime":"2024-11-22T19:07:07.063Z",
	  "name":"Usage Metrics Report",
	  "connectionType":"import",
	  "source":"Upload",
	  "datasets":[
        {
          "id":"0ad237c5-049b-4ea3-961a-4983b43fbefa",
		  "name":"Usage Metrics Report",
		  "webUrl":"https://app.powerbi.com/groups/me/datasets/0ad237c5-049b-4ea3-961a-4983b43fbefa",
		  "settingsUrl":"https://app.powerbi.com/groups/me/settings/datasets/0ad237c5-049b-4ea3-961a-4983b43fbefa",
		  "upstreamDatasets":[
            
          ],"users":[
            
          ]
        }
      ],"reports":[
        {
          "id":"dd328590-86a2-4a43-b5fd-b41da7c7bbce",
		  "reportType":"PowerBIReport",
		  "name":"Usage Metrics Report",
		  "webUrl":"https://app.powerbi.com/groups/me/reports/dd328590-86a2-4a43-b5fd-b41da7c7bbce",
		  "embedUrl":"https://app.powerbi.com/reportEmbed?reportId=dd328590-86a2-4a43-b5fd-b41da7c7bbce&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly9XQUJJLU5PUlRILUVVUk9QRS1FLVBSSU1BUlktcmVkaXJlY3QuYW5hbHlzaXMud2luZG93cy5uZXQiLCJlbWJlZEZlYXR1cmVzIjp7InVzYWdlTWV0cmljc1ZOZXh0Ijp0cnVlfX0%3d",
		  "isPbixLiveConnect":false,
		  "users":[
            
          ],"subscriptions":[
            
          ],"reportFlags":0
        }
      ],"dataflows":[
        
      ]
 
 
 */