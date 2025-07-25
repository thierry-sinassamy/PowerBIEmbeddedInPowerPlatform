#region using
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Tooling.Connector;
#endregion

namespace PowerBiEmbedder.Repository
{
    public class ContextServiceDataMappingPowerPlatform : IContextServiceDataMappingPowerPlatform
    {
        #region Properties

        public string NameServices { get; set; }
        public string ConnectedOrgFriendlyName { get; set; }
        public ServiceClient ServiceClientDataverse { get; set; }
        public CrmServiceClient CrmServiceClientDataverse { get; set; }
        public Guid UserId { get; set; }
        public string PowerPlatformUrl { get; set; }

        #endregion

        #region Constructor with parameters

        public ContextServiceDataMappingPowerPlatform(string nameServices, Guid userId, ServiceClient serviceClient, string connectedOrgFriendlyName, CrmServiceClient crmServiceClient, string powerPlatformUrl)
        {
            NameServices = nameServices;
            UserId = userId;
            ServiceClientDataverse = serviceClient;
            ConnectedOrgFriendlyName = connectedOrgFriendlyName;
            CrmServiceClientDataverse = crmServiceClient;
            PowerPlatformUrl = powerPlatformUrl;
        }

        #endregion
    }
}
