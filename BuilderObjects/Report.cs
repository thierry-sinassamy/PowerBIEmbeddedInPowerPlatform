using static PowerBiEmbedder.BuilderObjects.EnumReport;

namespace PowerBiEmbedder.BuilderObjects
{
    public class Report
    {
        private readonly ReportType _reportType;

        public Report(ReportType reportType)
        {
            this._reportType = reportType;
        }

        /* CONFIGURATION POWER BI in PBI Platform */
        public string? ReportNameInPBI { get; set; }
        public Guid? ReportGuidInPBI { get; set; }

        /* CONFIGURATION POWER BI in PP Platform - Dataverse */
        public string? Name { get; set; }
        public string? TabNameInPP { get; set; }
        public string? SectionNameInPP { get; set; }
        public string? SectionIdInPP { get; set; }
        public string? ClassIdInPP { get; set; }
        public string? GroupIdInPP { get; set; }
        public string? ReportIdInPP { get; set; }
        public string? ReportNameInPP { get; set; }
        public string? URLInPP { get; set; }
        public bool? FilterInPP { get; set; }
        public Guid? ReportGuidInPP { get; set; }
        public string? FilterTableInPP { get; set; }
        public string? FilterColumnInPP { get; set; }
        public string? FilterAliasDataverseInPP { get; set; }
        public bool? SectionShowLabel { get; set; }


        /*CONFIGURATION POWER PLATFORM COMING FROM TABLE SYSTEMFORM IN DATAVERSE*/
        public string? FormId { get; set; }
        public string? TabNameInXML { get; set; }
        public string? SectionNameInXML { get; set; }
        public string? ClassIdInXML { get; set; }
        public string? ControlIdInXML { get; set; }
        public string? PowerBIGroupIdInXML { get; set;}
        public string? PowerBIReportIdInXML { get; set; }
        public string? TileUrlInXML { get; set; }
        public string? PowerBIFilterInXML { get; set; }
    }
}
