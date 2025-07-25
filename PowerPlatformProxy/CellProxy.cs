using PowerBiEmbedder.PowerPlatformObject;

namespace PowerBiEmbedder.PowerPlatformProxy
{
    public class CellProxy
    {
        public object? Id { get; set; }
        public object? Rowspan { get; set; }
        public Control? CellControl { get; set; }
        public List<FormTabColumnSectionRowCellLabel>? CellLabels { get; set; }
        public string? SectionName { get; set; }
        public bool? SectionShowLabel { get; set; }
        public string? FromId { get; set; }        
    }
}
