using System.Xml.Serialization;

namespace PowerBiEmbedder.PowerPlatformObject
{
    public class FormTabColumnSectionRowCellLabel
    {
        [XmlAttribute("description")]
        public string? Description { get; set; }

        [XmlAttribute("languagecode")]
        public ushort LanguageCode { get; set; }
    }
}
