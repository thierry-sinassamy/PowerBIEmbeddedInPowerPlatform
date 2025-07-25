
using DataverseDataMappingModel;

namespace PowerBiEmbedder.ExecutionContext
{
    public class ValidatedContext
    {
        public ValidatedContext() { }

        public string? key { get; set; }
        public bool? validated { get; set; }
        public string? errorMessage { get; set; }
        public string? genericMessage { get; set; }
        public string? executionContext { get; set; }
        public string? powerPlatformMessage { get; set; }
        public string? projectName { get; set; }
        public Dictionary<string,string>? dictionaryReports { get; set; }
        public List<AmPlc_DataMappingPowerPlatform>? DataMappingPowerPlatforms { get; set; }
        public string? formAccountId { get; set; } //add the same property for the other entities if needed  (for example, for contact)
        public bool? processException { get; set; }
        
    }
}
