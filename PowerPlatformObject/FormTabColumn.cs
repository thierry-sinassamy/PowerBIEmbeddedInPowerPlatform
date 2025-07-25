using System.Xml.Serialization;

namespace PowerBiEmbedder.PowerPlatformObject
{
    public class FormTabColumn
    {
        [XmlArray("sections")]
        [XmlArrayItem("section")]
        public List<FormTabColumnSection>? Sections { get; set; }
    }
}
