using Application.Interfaces;
using Infrastructure.LogProvider;
namespace Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly Log _logProvider;

        public LogService(Log logProvider)
        {
            _logProvider = logProvider;
        }
        public void LogError(string message, Exception? ex = null) => _logProvider.Error(message, ex);
        public void LogiInfo(string message) => _logProvider.Info(message);
        public void LogWarn(string message) => _logProvider.Warn(message);

    }
}
