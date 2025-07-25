#region using
using DataverseDataMappingModel;
using PowerBiEmbedder.ExecutionContext;
using PowerBiEmbedder.PowerPlatformProxy;
#endregion

namespace PowerBiEmbedder.Util
{
    public static class ExtensionType
    {
        #region Build Dictionary

        /// <summary>
        /// Build a dictionary from the Control Proxy of the customizations.xml file [formXml] attribute in the dataverse (Table SystemForm).
        /// </summary>
        /// <param name="controlProxies"></param>
        /// <returns></returns>
        public static Dictionary<string, string> BuildDictionaryFromProxy(this List<ControlProxy> controlProxies)
        {
            if (controlProxies == null || (controlProxies != null && controlProxies.Count == 0)) { return null; }
            var dicProxy = new Dictionary<string, string>();

            foreach (var control in controlProxies)
            {
                if (dicProxy.ContainsKey(control.SectionName.ToString())) { continue; }
                dicProxy.Add(control.SectionName.ToString(), control.RowControl.Parameters.PowerBIReportId);
            }
            return dicProxy; 
        }

        /// <summary>
        /// Build a dictionary from the Table DataMappingPowerPlatform with the fields "SectionName" and "reportID".
        /// </summary>
        /// <param name="dataMappingPPs"></param>
        /// <returns></returns>
        public static Dictionary<string, string> BuildDictionaryFromDataMappingPowerPlatform(this List<AmPlc_DataMappingPowerPlatform> dataMappingPPs)
        {
            if(dataMappingPPs == null || (dataMappingPPs != null && dataMappingPPs.Count == 0)) { return null; }
            var dicDatMappingPP = new Dictionary<string, string>();

            foreach(var dmpp in dataMappingPPs)
            {
                if (dicDatMappingPP.ContainsKey(dmpp.AmPlc_SectionName)) { continue; }
                dicDatMappingPP.Add(dmpp.AmPlc_SectionName, dmpp.AmPlc_ReportId);
            }
            
            return dicDatMappingPP;
        }

        #endregion

        #region ValidatedContexte - Message

        public static void CompleteErrorMessage(this ValidatedContext validatedContext, string errorMessage)
        {
            if (validatedContext == null) { return; }

            validatedContext.errorMessage = errorMessage;
            validatedContext.validated = false;
        }

        public static void CompleteNoUpdateMessage(this ValidatedContext validatedContext, string noUpdateMessage)
        {
            if (validatedContext == null) { return; }

            validatedContext.genericMessage = noUpdateMessage + " - " + ObjectTypeCode.GenericMessage;
            validatedContext.validated = true;
        }

        #endregion

        #region Validation

        public static bool IsEqual(this string Value, string CompareValue)
        {
            bool ReturnValue = false;

            if (Value != null && CompareValue != null)
            {
                ReturnValue = string.Compare(Value.Trim(), CompareValue.Trim(), StringComparison.OrdinalIgnoreCase) == 0;
            }
            return ReturnValue;
        }

        #endregion
    }
}
