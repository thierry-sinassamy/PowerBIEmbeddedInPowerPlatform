#region using
using PowerBiEmbedder.PowerBiModel;
using PowerBiEmbedder.Repository;
using System.ServiceModel.Security;
using System.Text.Json;
#endregion

namespace PowerBiEmbedder.API
{
    public class ReportRepository : PowerBiRepository<PowerBiModel.Report>
    {
        public ReportRepository(IContextServicePowerBI powerBiService) : base(powerBiService)
        {
        }

        #region SecurityAccessDeniedException

        /// <summary>
        /// Forbidden for the service principal because API is only accessed by the Global Admin in Power BI.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SecurityAccessDeniedException"></exception>
        public override IEnumerable<PowerBiModel.Report> GetAll()
        {
            throw new SecurityAccessDeniedException("Get all the reports without the Group ID is not allowed in Power BI Service. Only the Administrator of Power Bi Service has the permission to execute this method."); //Send a custom exception to make sure we see it.
        }

        #endregion

        /// <summary>
        /// Get all reports in a specific group / workspace
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override IEnumerable<PowerBiModel.Report>? GetAll(string groupId)
        {
            var content = PowerBiService.Client.GetAsync($"{PowerBiService.PowerBiApiUrl}v1.0/myorg/groups/{groupId}/reports").Result.Content.ReadAsStringAsync();
            var contenu = content.Result.ToString();
            RootReport reports = new RootReport();
            reports.value = new List<PowerBiModel.Report>();
            if (contenu != string.Empty) { reports = JsonSerializer.Deserialize<RootReport>(contenu); }

            return reports?.value?.ToList();
        }

        /// <summary>
        /// Get a report using report ID in a specific group / workspace.
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public override PowerBiModel.Report? GetById(string groupId, string reportId)
        {
            var content = PowerBiService.Client.GetAsync($"{PowerBiService.PowerBiApiUrl}v1.0/myorg/groups/{groupId}/reports/{reportId}").Result.Content.ReadAsStringAsync();
            var contenu = content.Result.ToString();
            RootReport report = new RootReport();
            report.value = new List<PowerBiModel.Report>();
            if (contenu != string.Empty) { report = JsonSerializer.Deserialize<RootReport>(contenu); }

            return report?.value?.FirstOrDefault();
        }
    }
}
