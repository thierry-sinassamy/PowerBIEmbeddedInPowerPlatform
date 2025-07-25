using System.Xml.Serialization;

namespace PowerBiEmbedder.PowerPlatformObject
{
    public class FormTabColumnSection
    {
        [XmlArray("labels")]
        [XmlArrayItem("label")]
        public List<FormTabColumnSectionLabel>? Labels { get; set; }

        [XmlArray("rows")]
        [XmlArrayItem("row")]
        public List<Row>? Rows { get; set; }

        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlAttribute("showlabel")]
        public bool ShowLabel { get; set; }
    }
}
