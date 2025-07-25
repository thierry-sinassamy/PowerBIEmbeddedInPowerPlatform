#region using
using PowerBiEmbedder.ManagerService;
using static PowerBiEmbedder.BuilderObjects.EnumReport;
#endregion

namespace PowerBiEmbedder.BuilderObjects
{
    public class PowerBiReportBuilder : ReportBuilder
    {
        #region Constructor with parameters

        public PowerBiReportBuilder(IServiceProvider serviceProviders)
        {
            reports = new List<Report>((int)ReportType.PowerBI);
            services = serviceProviders;
        }

        #endregion

        #region Account

        /// <summary>
        /// Entity targeted : Account
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override IEnumerable<Report> BuildlReportAccount()
        {            
            var reportsPBIService = ManagerObject.GetReportByWorkspace(services, string.Empty);

            if(reportsPBIService == null) { return null; }

            foreach(var report in reportsPBIService)
            {
                var reportPBI = new Report(ReportType.PowerBI);
                reportPBI.ReportGuidInPBI = new Guid(report.id);
                reportPBI.ReportNameInPBI = report.name != string.Empty ? report.name: string.Empty;

                reports.Add(reportPBI);
            }            

            return reports;
        }

        #endregion
    }
}
