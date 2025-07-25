#region using
using PowerBiEmbedder.BuilderObjects;
using PowerBiEmbedder.ExecutionContext;
using PowerBiEmbedder.ObjectFactory;
using PowerBiEmbedder.ManagerService;
using Microsoft.Extensions.DependencyInjection;
using PowerBiEmbedder.Repository;
using PowerBiEmbedder.Util;
using System.Reflection;
using System;
#endregion

namespace PowerBiEmbedder.BuildingObject
{
    public class BuildingObjectAcc : IAbstractFactory
    {
        #region ProcessContextBuildingObject

        /// <summary>
        /// [ProcessContextBuildingObject] aims to determine if there is a difference between reports deployed in PBI Service and 
        /// reports deployed in Power Platform Environment (in table SystemForm).
        /// </summary>
        /// <param name="services"></param>
        /// <param name="validatedContext"></param>
        public void ProcessContextBuildingObject(IServiceProvider services, ref ValidatedContext validatedContext)
        {                    
            var builderInterface = new BuilderInterface();
            var service = services.GetRequiredService<DataverseDataMappingRepository<DataverseDataMappingModel.AmPlc_DataMappingLog>>();
            var qe = Util.Util.CreateQueryExpressionDataMappingProject(validatedContext.projectName);
            var project = services.GetRequiredService<DataverseDataMappingRepository<DataverseDataMappingModel.AmPlc_ProjectMarketingFAcTuRation>>().FindAll(qe).FirstOrDefault();
            
            try
            {
                var reportInPP = builderInterface.ConstructBuilder(new PowerPlatformReportBuilder(services));//Report for Power Platform              
                var reportInPBI = builderInterface.ConstructBuilder(new PowerBiReportBuilder(services));//report for Power BI            

                if(reportInPP == null || (reportInPP != null && reportInPP.Count == 0)) { return; }
                if (reportInPBI == null || (reportInPBI != null && reportInPBI.Count == 0)) { return; }

                //Call the compare method
                var dictionary = ManagerObject.CompareBuiltObjects(reportInPP, reportInPBI);
                if (dictionary != null && dictionary.Count > 0)
                {
                    validatedContext.dictionaryReports = new Dictionary<string, string>();
                    validatedContext.dictionaryReports = dictionary;

                    //get the account form ID
                    validatedContext.formAccountId = reportInPP.FirstOrDefault().FormId; //Id of the 1st in the list is enough.

                    //Complete with Data Mapping PowerPlatform
                    var dmpps = ManagerObject.GetDataMappingPPInPP(services);
                    validatedContext.DataMappingPowerPlatforms = new List<DataverseDataMappingModel.AmPlc_DataMappingPowerPlatform>();
                    validatedContext.DataMappingPowerPlatforms = dmpps;
                }
                else
                {
                    ExtensionType.CompleteNoUpdateMessage(validatedContext, MethodBase.GetCurrentMethod().Name);
                    var log = Util.Util.CreateEntityDataMappingLog(validatedContext.genericMessage, MethodBase.GetCurrentMethod().Name, project?.AmPlc_ProjectMarketingFAcTuRationId != null ? project?.AmPlc_ProjectMarketingFAcTuRationId.Value : Guid.Empty);
                    service.Create(log);
                }
            }
            catch (Exception ex)
            {                
                ExtensionType.CompleteErrorMessage(validatedContext, ex.ToString());
                var log = Util.Util.CreateEntityDataMappingLog(ex.ToString(), ex.StackTrace, project?.AmPlc_ProjectMarketingFAcTuRationId != null ? project?.AmPlc_ProjectMarketingFAcTuRationId.Value : Guid.Empty);                
                service.Create(log);                
            }
        }

        #endregion

        #region  ProcessReportPowerPlatform

        /// <summary>
        /// [ProcessReportPowerPlatform] aims to fix the reports in Power Platform according the reports in PBI Service.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="validatedContext"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ProcessReportPowerPlatform(IServiceProvider serviceProvider, ref ValidatedContext validatedContext)
        {            
           if(validatedContext.dictionaryReports != null && validatedContext.dictionaryReports.Count == 0){ return; } //Make sure reports
           if (validatedContext.DataMappingPowerPlatforms != null && validatedContext.DataMappingPowerPlatforms.Count == 0) { return; } //Make sure Data Mapping PP

           var reportsDmpps = Util.Util.GenerateReport(validatedContext.dictionaryReports, validatedContext.DataMappingPowerPlatforms);
           if(reportsDmpps != null && reportsDmpps.Count == 0) { return; }

           foreach(var r in reportsDmpps)
            {
                var sf = ManagerObject.HandleReportXmlForDataverse(r, validatedContext);
                serviceProvider.GetRequiredService<DataverseRepository<DataverseModel.SystemForm>>().Save(sf);

                var parameterXml = Util.Util.GenerateParameterEntity(ObjectTypeCode.systemform);
                var publishRequest = Util.Util.CreatePublishXmlRequest(parameterXml);

                serviceProvider.GetRequiredService<DataverseRepository<DataverseModel.SystemForm>>().Execute(publishRequest);
            }           
        }

        #endregion
    }
}
