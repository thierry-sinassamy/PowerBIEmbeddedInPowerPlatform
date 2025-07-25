#region using
using System.Net.Http.Headers;
#endregion

namespace PowerBiEmbedder.Repository
{
    public class ContextServicePowerBI : IContextServicePowerBI
    {
        #region Properties

        public string NameServices { get; set; }
        public string ConnectedOrgFriendlyName { get; set; }
        public string Token { get; set; }
        public string PowerBiUrl { get; set; } //https://analysis.windows.net/powerbi/api/.default
        public HttpClient Client { get; set; }
        public AuthenticationHeaderValue HeaderValue { get ; set ; }
        public string PowerBiApiUrl { get; set; } //https://api.powerbi.com/
        public string WorkspaceId { get; set; }
        public string WorkspaceName { get; set; }

        #endregion

        #region Constructor with parameters

        public ContextServicePowerBI(string nameServices, string connectedOrgFriendlyName, string token, string powerBiUrl, 
                                HttpClient client, AuthenticationHeaderValue headerValue, string powerBiApiUrl, string workspaceId, string workspaceName)
        {
            NameServices = nameServices;
            ConnectedOrgFriendlyName = connectedOrgFriendlyName;
            Token = token;
            PowerBiUrl = powerBiUrl;
            Client = client;
            HeaderValue = headerValue;
            PowerBiApiUrl = powerBiApiUrl;
            WorkspaceId = workspaceId;
            WorkspaceName = workspaceName;
        }

        #endregion
    }
}