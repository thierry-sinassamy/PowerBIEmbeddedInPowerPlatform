#region using
using PowerBiEmbedder.ExecutionContext;
using PowerBiEmbedder.ExecutionContext.ExecutionContextAction;
#endregion

namespace PowerBiEmbedder.ManagerService
{
    public class ManagerUseCaseValidation
    {
        List<IExecutionContext> _executionContexts = new List<IExecutionContext>();

        public ManagerUseCaseValidation() 
        {
            _executionContexts.Add(new Validate_Acc()); //account
            //add..the others
        }

        /// <summary>
        /// Validation of the context execution.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ValidatedContext ProcessExecutionValidation(string key)
        {
            var result = new ValidatedContext();
            if (_executionContexts.Count > 0)
            {
                for (var i = 0; i <= _executionContexts.Count - 1; i++)
                {
                    result = _executionContexts[i].ValidateExecutionContext(key);
                    if (result.validated.Value) { break; }
                }
            }
            else { result.errorMessage = ObjectTypeCode.MessageFromManagerValidation; }
            return result;
        }
    }
}
