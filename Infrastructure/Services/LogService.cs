using Application.Interfaces.IRepositories;
using Application.Interfaces.Services;

namespace Infrastructure.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepo;

        public LogService(ILogRepository logRepo)
        {
            _logRepo = logRepo;
        }

        public void LogError(string message, Exception? ex = null)
        {
            _logRepo.LogError(message, ex);
        }

        public void LogiInfo(string message)
        {
            _logRepo.LogInfo(message);
        }

        public void LogWarn(string message)
        {
            _logRepo.LogWarn(message);
        }
    }
}
