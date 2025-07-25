#region using
using DataverseDataMappingModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
#endregion

namespace PowerBiEmbedder.Repository
{
    public class DataMappingPowerPlatformRepository : DataverseDataMappingRepository<AmPlc_DataMappingPowerPlatform>
    {
        public DataMappingPowerPlatformRepository(IContextServiceDataMappingPowerPlatform context) : base(context)
        {
        }

        /// <summary>
        /// Get all Data mapping Platforms : Configuration of the XML in the Dataverse.
        /// </summary>
        /// <param name="qe"></param>
        /// <returns></returns>
        public override IEnumerable<AmPlc_DataMappingPowerPlatform>? FindAll(QueryExpression qe)
        {
            var dataMappingPPs = new List<AmPlc_DataMappingPowerPlatform>();           

            while (true)
            {
                EntityCollection enColl = ServiceClientDataverse.RetrieveMultiple(qe);
                if (enColl != null && enColl.Entities.Count > 0)
                {
                    for (var i = 0; i <= enColl.Entities.Count - 1; i++)
                    {
                        var ent = enColl.Entities[i].ToEntity<AmPlc_DataMappingPowerPlatform>();
                        dataMappingPPs.Add(ent);
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
            return dataMappingPPs;
        }
    }
}
