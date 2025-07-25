#region using
using DataverseDataMappingModel;
#endregion

namespace PowerBiEmbedder.Repository
{
    public class DataMappingLogRepository : DataverseDataMappingRepository<AmPlc_DataMappingLog>
    {
        public DataMappingLogRepository(IContextServiceDataMappingPowerPlatform context) : base(context)
        {
        }

        /// <summary>
        /// Create a log in the dataverse.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Guid Create(AmPlc_DataMappingLog entity)
        {
            return ServiceClientDataverse.Create(entity);            
        }
    }
}
