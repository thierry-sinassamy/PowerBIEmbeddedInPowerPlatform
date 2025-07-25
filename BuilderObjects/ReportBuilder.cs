namespace PowerBiEmbedder.BuilderObjects
{
    /// <summary>
    /// Abstract Interface for creating elements of the Report Object
    /// </summary>
    public abstract class ReportBuilder
    {
        protected List<Report>? reports;
        protected IServiceProvider? services;

        public List<Report> Reports
        {
            get { return reports; }
        }

        //abstract build methods, we can add more methods related to the report for each entity in the dataverse
        public abstract IEnumerable<Report> BuildlReportAccount();
    }
}
