using Microsoft.Extensions.Logging;

namespace Infrastructure.LogProvider
{
    public class Log
    {
        private readonly ILogger<Log> _logger;

        public Log(ILogger<Log> logger)
        {
            _logger = logger;
        }

        public void Info(string message) => _logger.LogInformation(message);
        public void Warn(string message) => _logger.LogWarning(message);
        public void Error(string message, Exception? ex = null) => _logger.LogError(ex, message);
    }
}
