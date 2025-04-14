namespace Application.Interfaces.IRepositories
{

    public interface ILogRepository
    {
        void LogError(string message, Exception? ex = null);
        void LogInfo(string message);
        void LogWarn(string message);
    }
}

