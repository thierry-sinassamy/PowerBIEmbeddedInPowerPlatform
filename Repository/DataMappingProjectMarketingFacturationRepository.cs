#region using
using DataverseDataMappingModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
#endregion

namespace PowerBiEmbedder.Repository
{
    public class DataMappingProjectMarketingFacturationRepository : DataverseDataMappingRepository<AmPlc_ProjectMarketingFAcTuRation>
    {
        public DataMappingProjectMarketingFacturationRepository(IContextServiceDataMappingPowerPlatform context) : base(context)
        {
        }

        /// <summary>
        /// Get the project related to the department.
        /// </summary>
        /// <param name="qe"></param>
        /// <returns></returns>
        public override IEnumerable<AmPlc_ProjectMarketingFAcTuRation>? FindAll(QueryExpression qe)
        {
            var projects = new List<AmPlc_ProjectMarketingFAcTuRation>();

            while (true)
            {
                EntityCollection enColl = ServiceClientDataverse.RetrieveMultiple(qe);
                if (enColl != null && enColl.Entities.Count > 0)
                {
                    for (var i = 0; i <= enColl.Entities.Count - 1; i++)
                    {
                        var ent = enColl.Entities[i].ToEntity<AmPlc_ProjectMarketingFAcTuRation>();
                        projects.Add(ent);
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
            return projects;
        }
    }
}
