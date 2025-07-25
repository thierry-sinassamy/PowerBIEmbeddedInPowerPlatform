#region using
using DataverseDataMappingModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xrm.Sdk;
using PowerBiEmbedder.API;
using PowerBiEmbedder.ExecutionContext;
using PowerBiEmbedder.PowerBiObject;
using PowerBiEmbedder.PowerPlatformObject;
using PowerBiEmbedder.PowerPlatformProxy;
using PowerBiEmbedder.Repository;
using PowerBiEmbedder.Util;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using static System.Collections.Specialized.BitVector32;
#endregion

namespace PowerBiEmbedder.ManagerService
{
    public static class ManagerObject
    {
        #region Work with Proxies

        /// <summary>
        /// Retrieve the value of the attribute "formxml" in the table "systemform" of the Dataverse.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public static FormProxy[] GetSystemForms(IServiceProvider serviceProvider)
        {
            var qe = Util.Util.CreateQueryExpressionSystemForm();

            var systemForm = serviceProvider.GetRequiredService<DataverseRepository<DataverseModel.SystemForm>>().FindAll(qe);
            
            var formProxies = systemForm
                                        .Select<Entity, FormProxy>((Func<Entity, FormProxy>)(f => new FormProxy(f)))
                                        .OrderBy<FormProxy, string>((Func<FormProxy, string>)(f => f.ToString()))
                                        .ToArray<FormProxy>();//convert entities into array of proxy
            return formProxies;
        }

        /// <summary>
        /// Load the tabs stored in the customizations.xml file, tabs containing all the embedded PBI reports.
        /// </summary>
        /// <param name="formXML"></param>
        /// <returns></returns>
        public static List<TabProxy> LoadTabs(string formXML, string formid)
        {
            if (formXML.Equals(string.Empty))
                return null;

            FormModel formModel;
            var tabProxies = new List<TabProxy>();

            using (StringReader stringReader = new StringReader(formXML))
            {
                formModel = (FormModel)new XmlSerializer(typeof(FormModel)).Deserialize((TextReader)stringReader);
            }

            if (formModel != null && formModel.Tabs.Count > 0)
            {
                foreach (FormTab tab in formModel.Tabs)
                {
                    tabProxies.Add(new TabProxy()
                    {
                        Text = tab.Labels.FirstOrDefault<FormTabLabel>()?.Description,
                        Value = (object)tab.Id,
                        Name = tab.Name != null ? tab.Name : string.Empty,
                        FormId = formid
                    });
                }
            }
            return tabProxies;
        }

        /// <summary>
        /// Load the sections stored in the customizations.xml file, sections containing all the embedded PBI reports.
        /// </summary>
        /// <param name="formXML"></param>
        /// <param name="tabProxies"></param>
        /// <returns></returns>
        public static List<SectionProxy> LoadSections(string formXML, List<TabProxy> tabProxies)
        {
            var sectionProxies = new List<SectionProxy>();
            FormModel formModel;

            using (StringReader stringReader = new StringReader(formXML))
            {
                formModel = (FormModel)new XmlSerializer(typeof(FormModel)).Deserialize((TextReader)stringReader);
            }
            if(formModel == null) { return null; }
            if (formModel.Tabs ==  null || (formModel.Tabs != null && formModel.Tabs.Count == 0)){ return null; }

            foreach (var tabProxy in tabProxies)
            {
                foreach (FormTabColumn column in formModel.Tabs.FirstOrDefault<FormTab>((Func<FormTab, bool>)(t => t.Id == tabProxy.Value.ToString())).Columns)
                {
                    foreach (FormTabColumnSection section in column.Sections)
                    {
                        sectionProxies.Add(new SectionProxy()
                        {
                            Id = section.Id,
                            Text = (section.Labels.FirstOrDefault<FormTabColumnSectionLabel>()?.Description == "" ? section.Name : section.Labels.FirstOrDefault<FormTabColumnSectionLabel>()?.Description + " (" + section.Name + ")"),
                            Section = section,
                            Name = section.Name,
                            ShowLabel = section.ShowLabel,
                            FormId = tabProxy.FormId
                        });
                    }
                }
            }          
            return sectionProxies;
        }

        #endregion

        #region Work with the Data Mapping PP

        /// <summary>
        /// Get the data mapping in the reference Dataverse : ISI-IT... for elements in Power Platform.
        /// </summary>
        /// <returns></returns>
        public static List<AmPlc_DataMappingPowerPlatform> GetDataMappingPPInPP(IServiceProvider serviceProvider)
        {
            var qe = Util.Util.CreateQueryExpressionDataMappingPowerPlatform();
            var dataMappingPP = serviceProvider.GetRequiredService<DataverseDataMappingRepository<AmPlc_DataMappingPowerPlatform>>().FindAll(qe);            

            return dataMappingPP.ToList(); 
        }

        #endregion

        #region Work with the Data Mapping PBI

        /// <summary>
        /// Get the data mapping in the reference Dataverse : ISI-IT...for PBI references in Power Platform.
        /// </summary>
        /// <returns></returns>
        public static List<AmPlc_DataMappingPowerBi> GetDataMappingPBIInPP(IServiceProvider serviceProvider)
        {
            var qe = Util.Util.CreateQueryExpressionDataMappingPowerBI();
            var dataMappingPBI = serviceProvider.GetRequiredService<DataverseDataMappingRepository<AmPlc_DataMappingPowerBi>>().FindAll(qe);

            return dataMappingPBI.ToList();
        }

        public static List<AmPlc_DataMappingEntityForms> GetDataMappingEntityForms(IServiceProvider serviceProvider)
        {
            //1-Get the Guid of AppConfigPowerBi in DataMappingPowerBi Table by name = AppConfigPowerBi
            var qe = Util.Util.CreateQueryExpressionDataMappingPowerBIByName(string.Empty); //pass the value AppConfigPowerBi
            var dataMappingPBIByName = serviceProvider.GetRequiredService<DataverseDataMappingRepository<AmPlc_DataMappingPowerBi>>().FindAll(qe).FirstOrDefault();

            if(dataMappingPBIByName == null) { throw new Exception("[dataMappingPBIByName] is NULL because not in the environment."); }

            //2-Get the list of DataMappingEntityForms by Guid
            var qeWithGuid = Util.Util.CreateQueryExpressionDataMappingEntityForms(dataMappingPBIByName.Id);
            var dataMappingEntityForms = serviceProvider.GetRequiredService<DataverseDataMappingRepository<AmPlc_DataMappingEntityForms>>().FindAll(qeWithGuid).ToList();

            return dataMappingEntityForms;
        }

        public static AmPlc_ProjectMarketingFAcTuRation GetProjectMarketingFacTuration(IServiceProvider serviceProvider)
        {
            var qe = Util.Util.CreateQueryExpressionDataMappingProject(Guid.Empty);//pass the guid of account
            var projects = serviceProvider.GetRequiredService<DataverseDataMappingRepository<AmPlc_ProjectMarketingFAcTuRation>>().FindAll(qe).ToList();

            //TODO

            //filter avec le paramètre dans appsetting
            return null;
        }

        #endregion

        #region Work with the Data in PBI Service

        public static List<PowerBiModel.Report> GetReportByWorkspace(IServiceProvider serviceProvider, string groupdId)
        {
            var reports = serviceProvider.GetRequiredService<ReportRepository>().GetAll(groupdId).ToList();            
            return reports;
        }

        #endregion

        #region Handle the reports PBI & PP

        /// <summary>
        /// Get the differences between PBI service and PowerPlatform service regarding each report deployed in PBI.
        /// If different reports, build a dictionary and retunr the latter.
        /// </summary>
        /// <param name="pbi"></param>
        /// <param name="pp"></param>
        /// <returns></returns>
        public static Dictionary<string, string> CompareBuiltObjects(List<PowerBiEmbedder.BuilderObjects.Report> pbi, List<PowerBiEmbedder.BuilderObjects.Report> pp)
        {
            var reports = new Dictionary<string, string>();

            for (var i = 0; i <= pbi.Count - 1; i++)
            {
                for (var j = 0; j <= pp.Count - 1; j++)
                {
                    if (pbi[i].ReportNameInPBI.IsEqual(pp[j].ReportNameInPP))
                    {
                        if (!pbi[i].ReportGuidInPBI.ToString().IsEqual(pp[j].ReportGuidInPP.ToString())) 
                        { 
                            reports.Add(pbi[i].ReportGuidInPBI.ToString(), pbi[i].ReportNameInPBI);                            
                        }
                    }
                    else { continue; }
                }
            }            
            return reports;
        }        

        /// <summary>
        /// Handle the Power BI section XML for the Dataverse before update the table systmeform (attribute "formxml").
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public static DataverseModel.SystemForm HandleReportXmlForDataverse(BuilderObjects.Report report, ValidatedContext validatedContext)
        {
            #region Local Variables

            var groupId = report.GroupIdInPP;
            var reportId = report.ReportIdInPP;
            var pbiUrl = report.URLInPP;
            var pbiTable = report.FilterTableInPP;
            var pbiColumn = report.FilterColumnInPP;
            var aliasDataverseField = report.FilterAliasDataverseInPP;
            var aliaddataverse = "<PowerBIFilter>" + new PbiFilter(pbiTable, pbiColumn, aliasDataverseField).ToJsonString() + "</PowerBIFilter>";
            var classId = report.ClassIdInPP;
            var rowspan = "1";
            var text4 = report.SectionNameInPP;
            var str6 = report.Name ?? text4.Replace(" ", "_");

            #endregion

            var powerBiSectionXml = "<section id=\"" + report.SectionIdInPP + "\" locklevel=\"0\" showlabel=\"" 
                            + report.SectionShowLabel.ToString().ToLower() + "\" IsUserDefined=\"0\" name=\"" 
                            + str6 + "\" labelwidth=\"115\" columns=\"1\" layout=\"varwidth\" showbar=\"false\"><labels><label description=\"" 
                            + text4 + "\" languagecode=\"1033\" /></labels><rows><row>" 
                            + string.Format("<cell id=\"{0:B}\" showlabel=\"true\" rowspan=\"{1}\" colspan=\"1\" auto=\"false\">", (object)Guid.NewGuid(), (object)rowspan) 
                            + "<labels><label description=\"Power BI Report\" languagecode=\"1033\" /></labels><control id=\"filteredreport\" classid=\""+ classId + "\"><parameters><PowerBIGroupId>"
                            + groupId + "</PowerBIGroupId><PowerBIReportId>" + reportId + "</PowerBIReportId><TileUrl>"
                            + pbiUrl + "/reportEmbed?reportId=" + reportId + "</TileUrl>" + aliaddataverse + "</parameters></control></cell></row></rows></section>";

            string _fetchXml = string.Empty;
            _fetchXml =  _fetchXml.Replace(new Regex("<section[^>]+" + report.SectionIdInPP + ".*?<\\/section>").Match(_fetchXml).Value, powerBiSectionXml);

            return new DataverseModel.SystemForm()
            {
                 FormId = new Guid(validatedContext.formAccountId),
                 FormXml= _fetchXml
            };            
        }

        #endregion

        #region Manage Log Message

        /// <summary>
        /// Handle the creation of a message into the Table [AmPlc_DataMappingLog].
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Guid CreateLogInPowerPlatform(IServiceProvider serviceProvider, AmPlc_DataMappingLog message)
        {
            return serviceProvider.GetRequiredService<DataverseDataMappingRepository<AmPlc_DataMappingLog>>().Create(message);           
        }

        #endregion
    }
}
