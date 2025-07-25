using System.Xml.Serialization;

namespace PowerBiEmbedder.PowerPlatformObject
{
    public class Cell
    {
        [XmlAttribute("id")]
        public string? Id { get; set; }

        [XmlAttribute("rowspan")]
        public string? RowSpan { get; set; }

        [XmlElement("control")]
        public Control? Control { get; set; }
        
        [XmlArray("labels")]
        [XmlArrayItem("label")]
        public List<FormTabColumnSectionRowCellLabel> Labels { get; set; }
    }
}
