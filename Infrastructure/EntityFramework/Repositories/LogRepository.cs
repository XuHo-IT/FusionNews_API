using Application.Interfaces.IRepositories;
using Infrastructure.LogProvider;

namespace Infrastructure.EntityFramework.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly Log _log;

        public LogRepository(Log log)
        {
            _log = log;
        }

        public void LogError(string message, Exception? ex = null)
        {
            _log.Error(message, ex);
        }

        public void LogInfo(string message)
        {
            _log.Info(message);
        }

        public void LogWarn(string message)
        {
            _log.Warn(message);
        }
    }
}
