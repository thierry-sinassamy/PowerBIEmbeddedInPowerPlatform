#region using
using DataverseModel;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
#endregion

namespace PowerBiEmbedder.Repository
{
    public class SystemFormRepository : DataverseRepository<SystemForm>
    {
        public SystemFormRepository(IContextServicePowerPlatform context) : base(context)
        {
        }

        /// <summary>
        /// Get all the systemforms related to entities in the dataverse.
        /// </summary>
        /// <param name="qe"></param>
        /// <returns></returns>
        public override IEnumerable<SystemForm>? FindAll(QueryExpression qe)
        {            
            return (IEnumerable<SystemForm>?)ServiceClientDataverse.RetrieveMultiple(qe).Entities;
        }

        public override void Save(SystemForm entity)
        {
            ServiceClientDataverse.Update(entity);
        }

        public override void Execute(PublishXmlRequest publishXmlRequest)
        {
            CrmServiceClientDataverse.Execute(publishXmlRequest); //Use of CrmServiceClient Class
        }
    }
}
