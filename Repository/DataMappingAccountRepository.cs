#region using
using DataverseDataMappingModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
#endregion

namespace PowerBiEmbedder.Repository
{
    public class DataMappingAccountRepository : DataverseDataMappingRepository<Account>
    {
        public DataMappingAccountRepository(IContextServiceDataMappingPowerPlatform context) : base(context)
        {
        }

        public override IEnumerable<Account>? FindAll(QueryExpression qe)
        {
            var accounts = new List<Account>();

            while (true)
            {
                EntityCollection enColl = ServiceClientDataverse.RetrieveMultiple(qe);
                if (enColl != null && enColl.Entities.Count > 0)
                {
                    for (var i = 0; i <= enColl.Entities.Count - 1; i++)
                    {
                        var ent = enColl.Entities[i].ToEntity<Account>();
                        accounts.Add(ent);
                    }
                }
                // Check for more records, if it returns true.
                if (enColl.MoreRecords)
                {
                    qe.PageInfo.PageNumber++;  // Increment the page number to retrieve the next page.                  
                    qe.PageInfo.PagingCookie = enColl.PagingCookie; // Set the paging cookie to the paging cookie returned from current results.
                }
                else
                {
                    break; //If no more records are in the result nodes, exit the loop.
                }
            }
            return accounts;
        }
    }
}
