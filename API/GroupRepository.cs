#region using
using PowerBiEmbedder.PowerBiModel;
using PowerBiEmbedder.Repository;
using System.Text.Json;
#endregion

namespace PowerBiEmbedder.API
{
    public class GroupRepository : PowerBiRepository<Group>
    {
        public GroupRepository(IContextServicePowerBI context) : base(context)
        {
        }

        /// <summary>
        /// Get all groups / workspaces in PBI service to which the service principal has access.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<Group>? GetAll()
        {
            var content = PowerBiService.Client.GetAsync($"{PowerBiService.PowerBiApiUrl}v1.0/myorg/groups").Result.Content.ReadAsStringAsync();           
            var contenu = content.Result.ToString();
            RootGroup groups = new RootGroup();
            groups.value = new List<PowerBiModel.Group>();
            if (contenu != string.Empty) { groups = JsonSerializer.Deserialize<RootGroup>(contenu); }

            return groups?.value?.ToList();
        }

        /// <summary>
        /// Get a specific group or workspace using an ID.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public override Group? GetById(string groupId)
        {
            var content = PowerBiService.Client.GetAsync($"{PowerBiService.PowerBiApiUrl}v1.0/myorg/groups/{groupId}").Result.Content.ReadAsStringAsync();
            var contenu = content.Result.ToString();
            RootGroup groups = new RootGroup();
            groups.value = new List<PowerBiModel.Group>();
            if (contenu != string.Empty) { groups = JsonSerializer.Deserialize<RootGroup>(contenu); }

            return groups?.value?.FirstOrDefault();
        }
    }
}
