using PowerBiEmbedder.PowerPlatformObject;

namespace PowerBiEmbedder.PowerPlatformProxy
{
    public class SectionProxy
    {
        public FormTabColumnSection? Section { get; set; }
        public string? Text { get; set; }
        public object? Id { get; set; }
        public string? Name { get; set; }
        public bool? ShowLabel { get; set; }
        public string? FormId { get; set; }
        public override string? ToString() => this.Text;
    }
}
