#region using
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerBiEmbedder.Repository;
using PowerBiEmbedder.Journal;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.PowerPlatform.Dataverse.Client;
using Azure.Identity;
using System.Net.Http.Headers;
using Microsoft.Xrm.Sdk.Query;
using PowerBiEmbedder.ExecutionContext;
using DataverseModel;
using PowerBiEmbedder.API;
using PowerBiEmbedder.PowerBiModel;
using DataverseDataMappingModel;
using static PowerBiEmbedder.BuilderObjects.EnumReport;
using System.Reflection;
#endregion

namespace PowerBiEmbedder.Util
{
    public static class Util
    {
        #region Get Services Context

        public static IContextServicePowerPlatform GetContextServicesFromPowerPlatform(string connectionString)
        {
            var service = new ServiceClient(connectionString);
            WhoAmIResponse whoAmIResponse = (WhoAmIResponse)service.Execute(new WhoAmIRequest());

            var organizationServiceClient = new CrmServiceClient(connectionString);

            return new ContextServicePowerPlatform("", whoAmIResponse.UserId, service, service.ConnectedOrgFriendlyName, organizationServiceClient, "");
        }

        public static IContextServiceDataMappingPowerPlatform GetContextServicesFromDataMappingPowerPlatform(string connectionString)
        {
            var service = new ServiceClient(connectionString);
            WhoAmIResponse whoAmIResponse = (WhoAmIResponse)service.Execute(new WhoAmIRequest());

            var organizationServiceClient = new CrmServiceClient(connectionString);

            return new ContextServiceDataMappingPowerPlatform("", whoAmIResponse.UserId, service, service.ConnectedOrgFriendlyName, organizationServiceClient, "");
        }

        public static IContextServicePowerBI GetContextServicesFromPowerBI(string tenantId, string clientId, string clientSecret)
        {
            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret); // A service principal
            var accessToken = credential.GetToken(new Azure.Core.TokenRequestContext(new[] { "https://analysis.windows.net/powerbi/api/.default" }));
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
            
            return new ContextServicePowerBI("", "", accessToken.Token, "Url", client, client.DefaultRequestHeaders.Authorization, "", "", "");
        }

        #endregion

        #region Dependency Injection - Service Provider

        public static IHostBuilder CreateHostBuilder(string[] strings, IContextServicePowerPlatform contextServicePowerPlatform,
                                        IContextServicePowerBI contextServicePowerBI, IContextServiceDataMappingPowerPlatform contextServiceDataMappingPowerPlatform)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((_, services) =>
                {
                    services.AddSingleton<LoggerMessage>();
                    //PowerPlatform environment as Target
                    services.AddSingleton<DataverseRepository<DataverseModel.SystemForm>>(new SystemFormRepository(contextServicePowerPlatform));

                    //PowerPlatform environment as Data Mapping
                    services.AddSingleton<DataverseDataMappingRepository<DataverseDataMappingModel.Account>>(new DataMappingAccountRepository(contextServiceDataMappingPowerPlatform));
                    services.AddSingleton<DataverseDataMappingRepository<DataverseDataMappingModel.AmPlc_DataMappingEntityForms>>(new DataMappingEntityFormsRepository(contextServiceDataMappingPowerPlatform));
                    services.AddSingleton<DataverseDataMappingRepository<DataverseDataMappingModel.AmPlc_DataMappingPowerBi>>(new DataMappingPowerBiRepository(contextServiceDataMappingPowerPlatform));
                    services.AddSingleton<DataverseDataMappingRepository<DataverseDataMappingModel.AmPlc_ProjectMarketingFAcTuRation>>(new DataMappingProjectMarketingFacturationRepository(contextServiceDataMappingPowerPlatform));
                    services.AddSingleton<DataverseDataMappingRepository<DataverseDataMappingModel.AmPlc_DataMappingPowerPlatform>>(new DataMappingPowerPlatformRepository(contextServiceDataMappingPowerPlatform));
                    services.AddSingleton<DataverseDataMappingRepository<DataverseDataMappingModel.AmPlc_DataMappingLog>>(new DataMappingLogRepository(contextServiceDataMappingPowerPlatform));

                    //PowerBI Service & Workspace / GroupId
                    services.AddSingleton<PowerBiRepository<Group>>(new GroupRepository(contextServicePowerBI));
                    services.AddSingleton<PowerBiRepository<Report>>(new ReportRepository(contextServicePowerBI));
                    services.AddSingleton<PowerBiRepository<PowerBiModel.Import>>(new ImportRepository(contextServicePowerBI));
                    services.AddSingleton<PowerBiRepository<Datasource>>(new DatasourceRepository(contextServicePowerBI));
                    services.AddSingleton<PowerBiRepository<Dataset>>(new DatasetRepository(contextServicePowerBI));
                });
        }

        #endregion

        #region QueryExpression

        public static QueryExpression CreateQueryExpressionSystemForm()
        {
            var qe = new QueryExpression(ObjectTypeCode.systemform);
            qe.ColumnSet = new ColumnSet();
            qe.ColumnSet.AllColumns = true;
            qe.Criteria.AddCondition(ObjectTypeCode.formxml, ConditionOperator.NotNull);
            qe.Criteria.AddCondition(ObjectTypeCode.name, ConditionOperator.Equal, ObjectTypeCode.Account); //type
            qe.Criteria.AddCondition(ObjectTypeCode.type, ConditionOperator.Equal, 2); //type
            qe.Criteria.AddCondition(ObjectTypeCode.objecttypecode, ConditionOperator.Equal, ObjectTypeCode.account);//objecttypecode
            qe.Criteria.AddCondition(ObjectTypeCode.componentstate, ConditionOperator.Equal, 0);//componentstate
            qe.Criteria.AddCondition(ObjectTypeCode.formactivationstate, ConditionOperator.Equal, 1);//formactivationstate

            return qe;
        }

        public static QueryExpression CreateQueryExpressionDataMappingPowerPlatform()
        {
            var qe = new QueryExpression(DataverseDataMappingModel.AmPlc_DataMappingPowerPlatform.EntityLogicalName);
            qe.ColumnSet = new ColumnSet();
            qe.ColumnSet.AllColumns = true;
            qe.Criteria.AddCondition(ObjectTypeCode.statecode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_DataMappingPowerPlatform_StateCode.Active);
            qe.Criteria.AddCondition(ObjectTypeCode.statuscode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_DataMappingPowerPlatform_StatusCode.Active);

            return qe;
        }

        /// <summary>
        /// QeuryExpression by name -> AppConfigPowerBi in the table "Data Mapping PowerBI"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static QueryExpression CreateQueryExpressionDataMappingPowerBI()
        {
            var qe = new QueryExpression(DataverseDataMappingModel.AmPlc_DataMappingPowerBi.EntityLogicalName);
            var qeFilter = new FilterExpression(LogicalOperator.And);
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statecode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_DataMappingPowerPlatform_StateCode.Active));
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statuscode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_DataMappingPowerPlatform_StatusCode.Active));            
            qe.Criteria = qeFilter;
            qe.ColumnSet = new ColumnSet();
            qe.ColumnSet.AllColumns = true;
            return qe;
        }

        /// <summary>
        /// QueryExpression by name -> AppConfigPowerBi in the table "Data Mapping PowerBI"
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static QueryExpression CreateQueryExpressionDataMappingPowerBIByName(string name)
        {
            var qe = new QueryExpression(DataverseDataMappingModel.AmPlc_DataMappingPowerBi.EntityLogicalName);
            var qeFilter = new FilterExpression(LogicalOperator.And);
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statecode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_DataMappingPowerPlatform_StateCode.Active));
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statuscode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_DataMappingPowerPlatform_StatusCode.Active));
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.name, ConditionOperator.Equal, name));
            qe.Criteria = qeFilter;
            qe.ColumnSet = new ColumnSet();
            qe.ColumnSet.AllColumns = true;
            return qe;
        }

        /// <summary>
        /// QueryExpression by Guid - Lookup -> AppConfigPowerBi in the table "Data Mapping Entity forms" is the lookup used for the search
        /// </summary>
        /// <param name="datamappingpbiID"></param>
        /// <returns></returns>
        public static QueryExpression CreateQueryExpressionDataMappingEntityForms(Guid datamappingpbiID)
        {
            var qe = new QueryExpression(DataverseDataMappingModel.AmPlc_DataMappingEntityForms.EntityLogicalName);          
            var qeFilter = new FilterExpression(LogicalOperator.And);
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statecode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_DataMappingEntityForms_StateCode.Active));
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statuscode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_DataMappingEntityForms_StatusCode.Active));
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.amplc_datamappingpbi, ConditionOperator.Equal, datamappingpbiID));
            qe.Criteria = qeFilter;
            qe.ColumnSet = new ColumnSet();
            qe.ColumnSet.AllColumns = true;

            return qe;
        }

        /// <summary>
        /// Table Project Marketing Facturation.
        /// </summary>
        /// <param name="datamappingpbiID"></param>
        /// <returns></returns>
        public static QueryExpression CreateQueryExpressionDataMappingProject(Guid accountId)
        {
            var qe = new QueryExpression(DataverseDataMappingModel.AmPlc_ProjectMarketingFAcTuRation.EntityLogicalName);
            var qeFilter = new FilterExpression(LogicalOperator.And);
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statecode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_ProjectMarketingFAcTuRation_StateCode.Active));
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statuscode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_ProjectMarketingFAcTuRation_StatusCode.Active));
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.amplc_account, ConditionOperator.Equal, accountId));

            qe.Criteria = qeFilter;
            qe.ColumnSet = new ColumnSet();
            qe.ColumnSet.AllColumns = true;

            return qe;
        }

        /// <summary>
        /// Queryexpression by Name -> in the Table AmPlc_ProjectMarketingFAcTuRation.
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static QueryExpression CreateQueryExpressionDataMappingProject(string projectName)
        {
            var qe = new QueryExpression(DataverseDataMappingModel.AmPlc_ProjectMarketingFAcTuRation.EntityLogicalName);
            var qeFilter = new FilterExpression(LogicalOperator.And);
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statecode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_ProjectMarketingFAcTuRation_StateCode.Active));
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.statuscode, ConditionOperator.Equal, DataverseDataMappingModel.AmPlc_ProjectMarketingFAcTuRation_StatusCode.Active));
            qeFilter.AddCondition(new ConditionExpression(ObjectTypeCode.name, ConditionOperator.Equal, projectName));

            qe.Criteria = qeFilter;
            qe.ColumnSet = new ColumnSet();
            qe.ColumnSet.AllColumns = true;

            return qe;
        }

        #endregion

        #region Entity/Object

        /// <summary>
        /// Create an object of type AmPlc_DataMappingLog.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static AmPlc_DataMappingLog CreateEntityDataMappingLog(string exception, string stackTrace, Guid? projectId)
        {
            return new AmPlc_DataMappingLog()
            {
                AmPlc_RelatedProject = new Microsoft.Xrm.Sdk.EntityReference(AmPlc_ProjectMarketingFAcTuRation.EntityLogicalName, projectId.Value),
                AmPlc_Name = "Log - " + projectId.ToString() + "-" + DateTime.UtcNow.ToLongDateString(),
                AmPlc_Description = "Coming from processus [" + stackTrace + "]",
                AmPlc_Exception = true,
                AmPlc_Message = exception
            };            
        }

        /// <summary>
        /// Define the XML to specify the entity to publish
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GenerateParameterEntity(string entityName)
        {
            return string.Format(@"<importexportxml><entities><entity>{0}</entity></entities></importexportxml>", entityName);
        }

        /// <summary>
        /// Returns an object of type [BuilderObjects.Report].
        /// </summary>
        /// <param name="dictionaryReports"></param>
        /// <param name="DataMappingPowerPlatforms"></param>
        /// <returns></returns>
        public static List<PowerBiEmbedder.BuilderObjects.Report> GenerateReport(Dictionary<string, string> dictionaryReports, List<AmPlc_DataMappingPowerPlatform> DataMappingPowerPlatforms)
        {
            var reports = new List<PowerBiEmbedder.BuilderObjects.Report>();

            foreach (var dic in dictionaryReports)
            {
                foreach (var dmpp in DataMappingPowerPlatforms)
                {
                    if(dic.Value.IsEqual(dmpp.AmPlc_ReportId))
                    {
                        var r = CreateReport(dmpp);
                        reports.Add(r);
                    }
                }
            }            
            return reports;            
        }

        #endregion

        #region PublishRequest

        /// <summary>
        /// Create the publish request for the repository
        /// </summary>
        /// <param name="parameterXml"></param>
        /// <returns></returns>
        public static PublishXmlRequest CreatePublishXmlRequest(string parameterXml)
        {
            var publishXmlRequest = new PublishXmlRequest() { ParameterXml = parameterXml};
            return publishXmlRequest;
        }

        #endregion

        #region Handle LogMessage

        public static AmPlc_DataMappingLog CreateLogMessageInPowerPlatform(string initialMessage, IServiceProvider services, ValidatedContext validatedContext)
        {            
            var description = string.Format("In the class [{0}] with Method [{1}]: Ending at : {2}.",
                                    nameof(Util), MethodBase.GetCurrentMethod()?.Name,
                                    DateTime.Now.ToLocalTime().ToString());

            var qe = Util.CreateQueryExpressionDataMappingProject(validatedContext.projectName);
            var project = services.GetRequiredService<DataverseDataMappingRepository<AmPlc_ProjectMarketingFAcTuRation>>().FindAll(qe).FirstOrDefault();
            var projectId = project != null ? project.Id : Guid.Empty;

            return new AmPlc_DataMappingLog()
            {
                AmPlc_Name = ObjectTypeCode.LogName + "-" + DateTime.Now.ToLocalTime().ToString(),
                AmPlc_RelatedProject = new Microsoft.Xrm.Sdk.EntityReference(AmPlc_ProjectMarketingFAcTuRation.EntityLogicalName, projectId),
                AmPlc_Description = description,
                AmPlc_Exception = validatedContext.processException,
                AmPlc_Message = initialMessage
            };
        }

        #endregion

        #region File

        /// <summary>
        /// Method to append text locally (on the server, not in the FTP server first).
        /// </summary>
        /// <param name="executingPathApplication"></param>
        /// <param name="logFileWithExtension"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static void AppendTextToLogFile(string executingPathApplication, string logFileWithExtension, string lineAppended)
        {
            try
            {
                using (var file = File.Open(executingPathApplication + logFileWithExtension, FileMode.Append, FileAccess.Write))
                using (var writer = new StreamWriter(file))
                {
                    writer.WriteLine(lineAppended);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Create the object of type [AmPlc_DataMappingPowerPlatform]
        /// </summary>
        /// <param name="dmpp"></param>
        /// <returns></returns>
        private static BuilderObjects.Report CreateReport(AmPlc_DataMappingPowerPlatform dmpp)
        {
            var report = new PowerBiEmbedder.BuilderObjects.Report(ReportType.PowerPlatform)
            {
                Name = dmpp.AmPlc_Name != string.Empty ? dmpp.AmPlc_Name : string.Empty,
                TabNameInXML = dmpp.AmPlc_TableName != string.Empty ? dmpp.AmPlc_TableName : string.Empty,
                SectionNameInXML = dmpp.AmPlc_SectionName != string.Empty ? dmpp.AmPlc_SectionName : string.Empty,
                SectionIdInPP = dmpp.AmPlc_SectionId != string.Empty ? dmpp.AmPlc_SectionId : string.Empty,
                ClassIdInPP = dmpp.AmPlc_ClassId != string.Empty ? dmpp.AmPlc_ClassId : string.Empty,
                GroupIdInPP = dmpp.AmPlc_GroupId != string.Empty ? dmpp.AmPlc_GroupId : string.Empty,
                ReportIdInPP = dmpp.AmPlc_ReportId != string.Empty ? dmpp.AmPlc_ReportId : string.Empty,
                ReportNameInPP = dmpp.AmPlc_ReportName != string.Empty ? dmpp.AmPlc_ReportName : string.Empty,
                URLInPP = dmpp.AmPlc_Url != string.Empty ? dmpp.AmPlc_Url : string.Empty,
                FilterInPP = dmpp.AmPlc_Filter.HasValue ? dmpp.AmPlc_Filter.Value : false,
                FilterTableInPP = dmpp.AmPlc_PBiTable != string.Empty ? dmpp.AmPlc_PBiTable : string.Empty,
                FilterColumnInPP = dmpp.AmPlc_PBiColumn != string.Empty ? dmpp.AmPlc_PBiColumn : string.Empty,
                FilterAliasDataverseInPP = dmpp.AmPlc_DatAverseField != string.Empty ? dmpp.AmPlc_DatAverseField : string.Empty
            };           
            return report;
        }

        #endregion
    }
}
