using PowerBiEmbedder.ExecutionContext;

namespace PowerBiEmbedder.ObjectFactory
{
    public class AbstractFactory : IAbstractFactory
    {
        public virtual void ProcessContextBuildingObject(IServiceProvider services, ref ValidatedContext validatedContext)
        {
            throw new NotImplementedException();
        }

        public virtual void ProcessReportPowerPlatform(IServiceProvider services, ref ValidatedContext validatedContext)
        {
            throw new NotImplementedException();
        }
    }
}