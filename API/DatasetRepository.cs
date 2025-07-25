#region using
using PowerBiEmbedder.PowerBiModel;
using PowerBiEmbedder.Repository;
using System.ServiceModel.Security;
using System.Text.Json;
#endregion

namespace PowerBiEmbedder.API
{
    public class DatasetRepository : PowerBiRepository<Dataset>
    {
        public DatasetRepository(IContextServicePowerBI powerBiService) : base(powerBiService)
        {
        }

        #region SecurityAccessDeniedException

        /// <summary>
        /// Forbidden for the service principal because API is only accessed by the Global Admin in Power BI.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SecurityAccessDeniedException"></exception>
        public override IEnumerable<Dataset>? GetAll()
        {
            throw new SecurityAccessDeniedException(""); //Send a custom exception to make sure we see it.
        }

        #endregion

        /// <summary>
        /// Retrieve the list of datasets inside a specific group (workspace).
        /// </summary>
        /// <param name="datasetId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override IEnumerable<Dataset>? GetAll(string groupId)
        {
            var content = PowerBiService.Client.GetAsync($"{PowerBiService.PowerBiApiUrl}v1.0/myorg/groups/{groupId}/datasets").Result.Content.ReadAsStringAsync();
            var contenu = content.Result.ToString();
            RootDataset datasets = new RootDataset();
            datasets.value = new List<PowerBiModel.Dataset>();
            if (contenu != string.Empty) { datasets = JsonSerializer.Deserialize<RootDataset>(contenu); }

            return datasets?.value?.ToList();
        }

        /// <summary>
        /// Retrieve the dataset by id in a specific group (workspace)
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="datasetId"></param>
        /// <returns></returns>
        public override PowerBiModel.Dataset? GetById(string groupId, string datasetId)
        {
            var content = PowerBiService.Client.GetAsync($"{PowerBiService.PowerBiApiUrl}v1.0/myorg/groups/{groupId}/datasets/{datasetId}").Result.Content.ReadAsStringAsync();
            var contenu = content.Result.ToString();
            RootDataset dataset = new RootDataset();
            dataset.value = new List<PowerBiModel.Dataset>();
            if (contenu != string.Empty) { dataset = JsonSerializer.Deserialize<RootDataset>(contenu); }

            return dataset?.value?.FirstOrDefault();
        }
    }
}
