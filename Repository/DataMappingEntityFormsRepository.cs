#region using
using DataverseDataMappingModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
#endregion

namespace PowerBiEmbedder.Repository
{
    public class DataMappingEntityFormsRepository : DataverseDataMappingRepository<AmPlc_DataMappingEntityForms>
    {
        public DataMappingEntityFormsRepository(IContextServiceDataMappingPowerPlatform context) : base(context)
        {
        }

        /// <summary>
        /// Get all the forms which are concerned.
        /// </summary>
        /// <param name="qe"></param>
        /// <returns></returns>
        public override IEnumerable<AmPlc_DataMappingEntityForms>? FindAll(QueryExpression qe)
        {
            var forms = new List<AmPlc_DataMappingEntityForms>();

            while (true)
            {
                EntityCollection enColl = ServiceClientDataverse.RetrieveMultiple(qe);
                if (enColl != null && enColl.Entities.Count > 0)
                {
                    for (var i = 0; i <= enColl.Entities.Count - 1; i++)
                    {
                        var ent = enColl.Entities[i].ToEntity<AmPlc_DataMappingEntityForms>();
                        forms.Add(ent);
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
            return forms;
        }
    }
}
