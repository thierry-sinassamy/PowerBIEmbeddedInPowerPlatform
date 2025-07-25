#region using
using System.Net.Http.Headers;
#endregion

namespace PowerBiEmbedder.Repository
{
    public interface IContextServicePowerBI
    {
        string NameServices { get; set; }
        string ConnectedOrgFriendlyName { get; set; }
        string Token { get; set; }
        string PowerBiUrl { get; set; } //https://analysis.windows.net/powerbi/api/.default
        string PowerBiApiUrl { get; set; }  //https://api.powerbi.com/
        HttpClient Client { get; set; }
        AuthenticationHeaderValue HeaderValue { get; set; }
        string WorkspaceId { get; set; }
        string WorkspaceName { get; set; }
    }
}
