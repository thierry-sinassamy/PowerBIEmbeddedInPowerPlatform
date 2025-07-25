namespace PowerBiEmbedder.ExecutionContext
{
    public class ObjectTypeCode
    {
        public const string MessageFromManagerValidation = "[ProcessExecutionValidation] Method has not been executed in the Class ManagerUseCaseValidation regarding the validation of the execution context !";
        public const string GenericMessage = "No Error and no difference has been detected during the process between Power Platform & Power BI Service !";
        public const string LogName = "CO-001";

        //Target as Entities related to the Form in which the IFrame will contain the embedded Power Bi report
        public const string account = "account";
        public const string contact = "contact";
        public const string account_contact = "account,contact";

        //QueryExpression SystemForm
        public const string formid = "formid";
        public const string Account = "Account";
        public const string systemform = "systemform";
        public const string formxml = "formxml";
        public const string name = "name";
        public const string type = "type";
        public const string objecttypecode = "objecttypecode";
        public const string componentstate = "componentstate";
        public const string formactivationstate = "formactivationstate";

        //QueryExpression DataMappingPowerPlatform / DataMappingPowerBI
        public const string amplc_datamappingpowerplatform = "amplc_datamappingpowerplatform";
        public const string amplc_datamappingpowerbi = "amplc_datamappingpowerbi";        
        public const string amplc_datamappingentityforms = "amplc_datamappingentityforms";
        public const string statecode = "statecode";
        public const string statuscode = "statuscode";
        public const string amplc_datamappingpbi = "amplc_datamappingpbi";
        public const string amplc_account = "amplc_account";        
    }
}
