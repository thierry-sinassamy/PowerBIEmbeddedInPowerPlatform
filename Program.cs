#region using

using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Extensions.Configuration;
using PowerBiEmbedder;
using Microsoft.PowerPlatform.Dataverse.Client.Utils;
using PowerBiEmbedder.Util;
using PowerBiEmbedder.ManagerService;
using PowerBiEmbedder.ObjectFactory;
using Microsoft.Extensions.DependencyInjection;
using Journal = PowerBiEmbedder.Journal.LoggerMessage;
using System.Net;
using PowerBiEmbedder.ExecutionContext;

#endregion

#region Configuration

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); //to update before deploying usng batch file.

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())               
               .AddJsonFile($"appsettings.{environment}.json", true, true);

var configurationRoot = builder.Build();
var appConfig = configurationRoot.GetSection(nameof(AppConfigPowerPlatform)).Get<AppConfigPowerPlatform>();
if(appConfig == null) { return; }
string connectionString = "AuthType=" + appConfig.AuthType + ";" + "ClientId='" + appConfig.ClientId 
                                + "';" + "ClientSecret='" + appConfig.ClientSecret + "';" + "Url='" + appConfig.PowerPlatformUrl + "'";

string connectionStringDataMapping = "AuthType=" + appConfig.AuthType + ";" + "ClientId='" 
                                    + appConfig.ClientId + "';" + "ClientSecret='" 
                                    + appConfig.ClientSecret + "';" + "Url='" 
                                    + appConfig.PowerPlatformDataMappingUrl + "'"; 
var logFile = appConfig.LogFile;

#endregion

#region Local Variables

IServiceProvider services = null;
var initialMessage = string.Empty;
var serviceException = false;
var validatedContext = new ValidatedContext();
var sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

#endregion

try
{
    #region Services

    try {
        var contextServicesPP = Util.GetContextServicesFromPowerPlatform(connectionString);
        var contextServicesDMPP = Util.GetContextServicesFromDataMappingPowerPlatform(connectionStringDataMapping);
        var contextServicePBI = Util.GetContextServicesFromPowerBI("tenantId", "clientId", "clientSecret");

        services = ManagerServices.GenerateServiceProvider(args, contextServicesPP, contextServicePBI, contextServicesDMPP); //Instanciate the dependency for repository
    }
    catch(Exception ex)
    {
        serviceException = true;
        initialMessage= string.Format("[serviceException] with message [{0}] and with stacktrace [{1}]", ex.Message, ex.StackTrace);
    }
    
    #endregion

    #region Process

    validatedContext = Manager.ValidateConfigurationAsEntryPoint(string.Empty); //Keyvfrom appsetting - TODO
    if (validatedContext.validated.HasValue && !validatedContext.validated.Value) { return; }

    var factory = new FactoryContextBuildingObject();
    var factoryInstanciated = factory.InstanciateBuildingObject(validatedContext.key);
    Manager.StartProcessBuildingObject(factoryInstanciated, services, validatedContext);
    Manager.EndProcessReportPowerPlatform(factoryInstanciated, services, validatedContext);

    initialMessage = string.Format("[Process] ended at [{0}] and with sucess.", DateTime.Now.ToLocalTime().ToString());

    #endregion
}
catch (Exception ex)
{
    validatedContext.processException = true;
    initialMessage = string.Format("[serviceException] with message [{0}] and with stacktrace [{1}]", ex.Message, ex.StackTrace);
    services.GetRequiredService<Journal>().AppendText(ref initialMessage, string.Format("Exception happened as generic error at {0} : {1}.", 
                                            DateTime.Now.ToLocalTime().ToString(), ex.Message));
}
finally
{
    if (!serviceException)
    {
        services.GetRequiredService<Journal>().AppendText(ref initialMessage, string.Format("Exception might happen as generic error at {0} : {1}.",
                                            DateTime.Now.ToLocalTime().ToString(), initialMessage));
        var logMessage = Util.CreateLogMessageInPowerPlatform(initialMessage, services, validatedContext);
        ManagerObject.CreateLogInPowerPlatform(services, logMessage); //CRUD
    }               
    Util.AppendTextToLogFile(sCurrentDirectory, logFile, initialMessage);//Text File Log
}

#region Code in comments
/*
try
{
    var serviceClient = new ServiceClient(connectionString);
    if (serviceClient.IsReady)
    {
        Console.WriteLine("Connected to Dataverse!");        
    }
    else
    {
        Console.WriteLine("Failed to connect to Dataverse.");
    }
}
catch (DataverseConnectionException ex)
{
    Console.WriteLine(ex.Message);
    Console.WriteLine(ex.InnerException);
}
*/
#endregion
