using System.Xml.Serialization;

namespace PowerBiEmbedder.PowerPlatformObject
{
    public class Parameters
    {
        [XmlElement("PowerBIGroupId")]
        public string? PowerBIGroupId { get; set; }

        [XmlElement("PowerBIReportId")]
        public string? PowerBIReportId { get; set; }

        [XmlElement("TileUrl")]
        public string? TileUrl { get; set; }

        [XmlElement("PowerBIFilter")]
        public string? PowerBIFilter { get; set; }
    }
}
