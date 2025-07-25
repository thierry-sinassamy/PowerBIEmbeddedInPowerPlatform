
namespace PowerBiEmbedder.ExecutionContext.ExecutionContextAction
{
    public class Validate_Acc : IExecutionContext
    {
        /// <summary>
        /// Validate if in the configuration file (appsettings.json) with ONLY "account" in the parameter "OBJECTTYPECODE" & "NAME"
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ValidatedContext? ValidateExecutionContext(string key)
        {
            if (string.IsNullOrEmpty(key)) return null;
            var result = new ValidatedContext();

            if (key.ToLower() != ObjectTypeCode.account.ToLower()) { return result; }

            result.executionContext = ObjectTypeCode.account.ToLower();
            result.validated = true;
            result.key = key;

            return result;
        }
    }
}
