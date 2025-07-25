namespace PowerBiEmbedder.PowerPlatformProxy
{
    public class TabProxy
    {
        public string? Text { get; set; }
        public object? Value { get; set; }
        public string? Name { get; set; }
        public string? FormId { get; set; }
        public override string? ToString() => this.Text;
    }
}
