using System.Xml.Serialization;

namespace PowerBiEmbedder.PowerPlatformObject
{
    public class Row
    {
        [XmlElement("cell")]
        public List<Cell>? Cells { get; set; }
    }
}
