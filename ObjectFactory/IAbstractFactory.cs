using PowerBiEmbedder.ExecutionContext;

namespace PowerBiEmbedder.ObjectFactory
{
    public interface IAbstractFactory
    {
        void ProcessContextBuildingObject(IServiceProvider services, ref ValidatedContext validatedContext);

        void ProcessReportPowerPlatform(IServiceProvider services, ref ValidatedContext validatedContext);
    }
}
