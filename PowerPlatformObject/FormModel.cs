using System.Xml.Serialization;

namespace PowerBiEmbedder.PowerPlatformObject
{
    [XmlRoot("form")]
    public class FormModel
    {
        [XmlArray("tabs")]
        [XmlArrayItem("tab")]
        public List<FormTab>? Tabs { get; set; }

        [XmlAttribute("showImage")]
        public bool ShowImage { get; set; }
    }
}
