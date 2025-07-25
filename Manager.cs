#region using

using PowerBiEmbedder.ExecutionContext;
using PowerBiEmbedder.ManagerService;
using PowerBiEmbedder.ObjectFactory;

#endregion

namespace PowerBiEmbedder
{
    public static class Manager
    {
        #region Use Case Validation

        /// <summary>
        /// The method as entry point to validate the configuration json file with the data in the environment ISI-IT-MAIN-DEV,...
        /// If the content of the jaon file is different of the content in the dataverse ISI-IT-MAIN..., then generate an exception and create a record in the environment.
        /// </summary>
        public static ValidatedContext ValidateConfigurationAsEntryPoint(string key)
        {
            return new ManagerUseCaseValidation().ProcessExecutionValidation(key);
        }

        #endregion

        #region Build Object PP & PBI

        /// <summary>
        /// The method [StartProcessBuildingObject] instanciates the factory and starts the process of validating if there is a difference between PP and PBI.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="validatedContext"></param>
        public static void StartProcessBuildingObject(IAbstractFactory factoryInstanciated, IServiceProvider serviceProvider, ValidatedContext validatedContext)
        {
            factoryInstanciated.ProcessContextBuildingObject(serviceProvider, ref validatedContext);
        }

        #endregion

        #region Handle Reports in PP from PBI

        public static void EndProcessReportPowerPlatform(IAbstractFactory factoryInstanciated, IServiceProvider serviceProvider, ValidatedContext validatedContext)
        {
            factoryInstanciated.ProcessReportPowerPlatform(serviceProvider, ref validatedContext);
        }

        #endregion
    }
}
