namespace Application.Interfaces.Services
{
    public interface ILogService
    {
        void LogiInfo(string message);
        void LogWarn(string message);
        void LogError(string message, Exception? ex = null);
    }
}
