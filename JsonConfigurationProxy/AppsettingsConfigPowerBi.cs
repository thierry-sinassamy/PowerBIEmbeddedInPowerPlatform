namespace PowerBiEmbedder.JsonConfigurationProxy
{
    /// <summary>
    /// Proxy for appsettings configuration file (Json) - appsettings.json : tag AppConfigPowerBi
    /// </summary>
    public class AppsettingsConfigPowerBi
    {        
        public string? TenantId { get; set; }
        public string? GroupId { get; set; }
        public string? WorkspaceId { get; set; }
        public string? WorkspaceName { get; set; }
        public string? Classid { get; set; }

        //Sales Report
        public string? NameSalesReport { get; set; }
        public string? PbiTableSalesReport { get; set; }
        public string? PbiColumnSalesReport { get; set; }
        public string? CdsFieldSalesReport { get; set; }

        //Order Report
        public string? NameOrderReport { get; set; }
        public string? PbiTableOrderReport { get; set; }
        public string? PbiColumnOrderReport { get; set; }
        public string? CdsFieldOrderReport { get; set; }

        //Complaint Report
        public string? NameComplaintReport { get; set; }
        public string? PbiTableComplaintReport { get; set; }
        public string? PbiColumnComplaintReport { get; set; }
        public string? CdsFieldComplaintReport { get; set; }

        //Credit Info report
        public string? NameCreditInfoReport { get; set; }
        public string? PbiTableCreditInfoReport { get; set; }
        public string? PbiColumnCreditInfoReport { get; set; }
        public string? CdsFieldCreditInfotReport { get; set; }        
    }
}
