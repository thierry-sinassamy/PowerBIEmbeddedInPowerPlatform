#region using
using PowerBiEmbedder.PowerBiModel;
using PowerBiEmbedder.Repository;
using System.ServiceModel.Security;
using System.Text.Json;
#endregion

namespace PowerBiEmbedder.API
{
    public class DatasourceRepository : PowerBiRepository<Datasource>
    {
        public DatasourceRepository(IContextServicePowerBI powerBiService) : base(powerBiService)
        {
        }

        #region SecurityAccessDeniedException

        /// <summary>
        /// Forbidden for the service principal because API is only accessed by the Global Admin in Power BI.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SecurityAccessDeniedException"></exception>
        public override IEnumerable<Datasource>? GetAll()
        {
            throw new SecurityAccessDeniedException(""); //Send a custom exception to make sure we see it.
        }

        #endregion

        /// <summary>
        /// Retrieve the list of datasources of a specific dataset inside a specific group (workspace).
        /// </summary>
        /// <param name="datasetId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override IEnumerable<Datasource>? GetAll(string groupId, string datasetId)
        {
            var content = PowerBiService.Client.GetAsync($"{PowerBiService.PowerBiApiUrl}v1.0/myorg/groups/{groupId}/datasets/{datasetId}/datasources").Result.Content.ReadAsStringAsync();
            var contenu = content.Result.ToString();
            RootDatasource datasource = new RootDatasource();
            datasource.value = new List<PowerBiModel.Datasource>();
            if (contenu != string.Empty) { datasource = JsonSerializer.Deserialize<RootDatasource>(contenu); }

            return datasource?.value?.ToList();
        }
    }
}
