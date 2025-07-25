using Microsoft.Extensions.Logging;

namespace PowerBiEmbedder.Journal
{
    public class LoggerMessage
    {
        private readonly ILogger<LoggerMessage> _logger;

        public LoggerMessage(ILogger<LoggerMessage> logger)
        {
            _logger = logger;
        }

        public void LogMessage(string message)
        {
            _logger.LogInformation(message);
        }

        public void AppendText(ref string intial, string append)
        {

            intial = intial + "\n" + append;
        }
    }
}
