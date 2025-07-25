using System.Xml.Serialization;

namespace PowerBiEmbedder.PowerPlatformObject
{
    public class FormTab
    {
        [XmlArray("labels")]
        [XmlArrayItem("label")]
        public List<FormTabLabel>? Labels { get; set; }

        [XmlArray("columns")]
        [XmlArrayItem("column")]
        public List<FormTabColumn>? Columns { get; set; }

        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("name")]
        public string? Name { get; set; }
    }
}
