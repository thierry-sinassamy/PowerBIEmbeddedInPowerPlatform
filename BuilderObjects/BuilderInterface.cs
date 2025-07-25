namespace PowerBiEmbedder.BuilderObjects
{
    public class BuilderInterface
    {
        public BuilderInterface() { }

        /// <summary>
        /// Interface to build reports for each entity in the dataverse and to build all type of reports (PP, PBI, etc...).
        /// </summary>
        /// <param name="reportBuilder"></param>
        /// <returns></returns>
        public List<Report> ConstructBuilder(ReportBuilder reportBuilder)
        {
            var reports = reportBuilder.BuildlReportAccount(); //for account
            //etc...for the next entities (contact, ...)

            return (List<Report>)reports;
        }
    }
}
