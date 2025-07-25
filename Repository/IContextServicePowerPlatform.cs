#region using
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Tooling.Connector;
#endregion

namespace PowerBiEmbedder.Repository
{
    public interface IContextServicePowerPlatform
    {
        string NameServices { get; set; }
        string ConnectedOrgFriendlyName { get; set; }
        ServiceClient ServiceClientDataverse { get; set; }
        CrmServiceClient CrmServiceClientDataverse { get; set; }
        Guid UserId { get; set; }
        string PowerPlatformUrl { get; set; }
    }
}
