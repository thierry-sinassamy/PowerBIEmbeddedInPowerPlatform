#region using
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PowerBiEmbedder.Repository;
#endregion

namespace PowerBiEmbedder.ManagerService
{
    public static class ManagerServices
    {
        /// <summary>
        /// Inject all the services in IServiceProvider.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="contextServicePowerPlatform"></param>
        /// <param name="contextServicePowerBI"></param>
        /// <param name="contextServiceDataMappingPowerPlatform"></param>
        /// <returns></returns>
        public static IServiceProvider GenerateServiceProvider(string[] args, 
                            IContextServicePowerPlatform contextServicePowerPlatform, IContextServicePowerBI contextServicePowerBI, 
                            IContextServiceDataMappingPowerPlatform contextServiceDataMappingPowerPlatform)
        {
            IHost host = Util.Util.CreateHostBuilder(args, contextServicePowerPlatform, contextServicePowerBI, contextServiceDataMappingPowerPlatform).Build();
            var scope = host.Services.CreateScope();
            return scope.ServiceProvider;
        }
    }
}
