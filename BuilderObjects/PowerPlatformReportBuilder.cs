#region using
using PowerBiEmbedder.ExecutionContext;
using PowerBiEmbedder.ManagerService;
using PowerBiEmbedder.PowerPlatformObject;
using PowerBiEmbedder.PowerPlatformProxy;
using PowerBiEmbedder.Util;
using static PowerBiEmbedder.BuilderObjects.EnumReport;
#endregion

namespace PowerBiEmbedder.BuilderObjects
{
    public class PowerPlatformReportBuilder : ReportBuilder
    {
        #region Constructor with parameters

        public PowerPlatformReportBuilder(IServiceProvider serviceProviders)
        {
            reports = new List<Report>((int)ReportType.PowerPlatform);
            services = serviceProviders;
        }

        #endregion

        #region Account

        /// <summary>
        /// Entity targeted : Account
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override IEnumerable<Report> BuildlReportAccount()
        {
            #region Proxy from customization.xml

            var formProxies = ManagerObject.GetSystemForms(services);

            if (formProxies.Any() && formProxies.Count() > 1) { throw new Exception("More than 1 FormProxy for the Account Entity"); }
            var formXml = formProxies[0].Entity.Attributes[ObjectTypeCode.formxml].ToString();

            var tabProxies = ManagerObject.LoadTabs(formXml, formProxies[0].Entity.Attributes[ObjectTypeCode.formid].ToString());//Make sure to get the formId of the account form stored in the dataverse.

            if (tabProxies == null) { return null; }

            var sectionProxies = ManagerObject.LoadSections(formXml, tabProxies);

            if (sectionProxies == null) { return null; }

            #endregion

            #region Tabs & Sections from Data Mapping PowerPlatform Table & Filter the sectionProxies with Section Name in DataMapping PowerPlatform Table

            var tabsAndsections = ManagerObject.GetDataMappingPPInPP(services);
            if(tabsAndsections == null) { return null; }

            var sections = new List<string>();                       
            foreach(var section in tabsAndsections)
            {
                if(sections.Contains(section.AmPlc_SectionName)) { continue;  }
                sections.Add(section.AmPlc_SectionName);                
            }

            if(sections!= null && sections.Count == 0) { return null; }

            var sectionProxyFilteredList = new List<SectionProxy>();

            foreach(var sec in sections)
            {
                var sectionProxyFiltered = new SectionProxy();
                sectionProxyFiltered = sectionProxies.Where(s => s.Section.Name == sec).FirstOrDefault();
                if (sectionProxyFilteredList.Contains(sectionProxyFiltered)) { continue; }
                sectionProxyFilteredList.Add(sectionProxyFiltered);
            }

            #endregion

            #region Finalize the proxies after filtering - Use the sectionProxyFilteredList

            var rowProxies = new List<RowProxy>();
            foreach (var sectionProxy in sectionProxyFilteredList)
            {
                foreach (Row sectionRow in sectionProxy.Section.Rows) { 
                    rowProxies.Add(new RowProxy(){ 
                        SectionRow = sectionRow, 
                        SectionName = sectionProxy.Name,
                        SectionShowLabel = sectionProxy.ShowLabel,
                        FromId = sectionProxy.FormId
                    }); 
                }
            }

            if(rowProxies == null || rowProxies.Count == 0) { return null; }

            var cellProxies = new List<CellProxy>();
            foreach (var rowProxy in rowProxies)
            {
                foreach (Cell cellrow in rowProxy.SectionRow.Cells)
                {
                    cellProxies.Add(new CellProxy()
                    {
                        Id = cellrow.Id,
                        Rowspan = cellrow.RowSpan,
                        CellControl = cellrow.Control,
                        CellLabels = cellrow.Labels,
                        SectionName = rowProxy.SectionName,
                        SectionShowLabel = rowProxy.SectionShowLabel,
                        FromId = rowProxy.FromId
                    });
                }
            }

            if(cellProxies == null || cellProxies.Count == 0) { return null; }

            var controlProxies = new List<ControlProxy>();
            foreach (var cellProxy in cellProxies) { controlProxies.Add(new ControlProxy() { RowControl = cellProxy.CellControl, SectionName = cellProxy.SectionName }); }

            if(controlProxies == null || controlProxies.Count == 0) { return null; }
            #endregion

            #region Generation of dictionaries before comparison           

            var dicProxies = controlProxies.BuildDictionaryFromProxy();
            var dicDatMappingPPs = tabsAndsections.BuildDictionaryFromDataMappingPowerPlatform();

            if(dicProxies!= null && dicProxies.Count == 0) { return null; }
            if (dicDatMappingPPs != null && dicDatMappingPPs.Count == 0) { return null; }

            #endregion

            #region Build report

            foreach(var dic in dicProxies)
            {                
                var reportPP = new Report(ReportType.PowerPlatform);            
                reportPP.PowerBIReportIdInXML= dic.Value != string.Empty ? dic.Value : string.Empty;
                reportPP.SectionNameInXML = dic.Key != string.Empty ? dic.Key: string.Empty;
                reportPP.FormId = formProxies[0].Entity.Attributes[ObjectTypeCode.formid].ToString() != string.Empty ? 
                                    formProxies[0].Entity.Attributes[ObjectTypeCode.formid].ToString() : string.Empty;

                reports.Add(reportPP);
            }            

            #endregion

            return reports;
        }

        #endregion
    }
}
