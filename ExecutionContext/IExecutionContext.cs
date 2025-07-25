namespace PowerBiEmbedder.ExecutionContext
{
    public interface IExecutionContext
    {
        ValidatedContext? ValidateExecutionContext(string key);
    }
}
