using System.Xml.Serialization;

namespace PowerBiEmbedder.PowerPlatformObject
{
    public class Control
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("classid")]
        public string? ClassId { get; set; }

        [XmlElement("parameters")]
        public Parameters? Parameters { get; set; }
    }
}
