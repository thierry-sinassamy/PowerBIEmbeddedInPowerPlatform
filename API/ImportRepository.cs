#region using
using PowerBiEmbedder.PowerBiModel;
using PowerBiEmbedder.Repository;
using System.ServiceModel.Security;
using System.Text.Json;
#endregion

namespace PowerBiEmbedder.API
{
    public class ImportRepository : PowerBiRepository<Import>
    {
        public ImportRepository(IContextServicePowerBI powerBiService) : base(powerBiService)
        {
        }

        #region SecurityAccessDeniedException

        /// <summary>
        /// Forbidden for the service principal because API is only accessed by the Global Admin in Power BI.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SecurityAccessDeniedException"></exception>
        public override IEnumerable<Import> GetAll()
        {
            throw new SecurityAccessDeniedException(""); //Send a custom exception to make sure we see it.
        }

        #endregion

        /// <summary>
        /// Retrieve the list of imports of a specific group (workspace).
        /// </summary>
        /// <param name="datasetId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override IEnumerable<Import>? GetAll(string groupId)
        {
            var content = PowerBiService.Client.GetAsync($"{PowerBiService.PowerBiApiUrl}v1.0/myorg/groups/{groupId}/imports").Result.Content.ReadAsStringAsync();
            var contenu = content.Result.ToString();
            RootImport imports = new RootImport();
            imports.value = new List<PowerBiModel.Import>();
            if (contenu != string.Empty) { imports = JsonSerializer.Deserialize<RootImport>(contenu); }

            return imports?.value?.ToList();
        }
    }
}
