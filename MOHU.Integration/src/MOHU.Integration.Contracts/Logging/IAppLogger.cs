namespace MOHU.Integration.Contracts.Logging
{
    public interface IAppLogger
    {
        Task LogError(string message, params object[] args);
        Task LogError(Exception exception);
        Task LogError(string messageTemplate, Exception exception);
        Task LogInfo(string message, params object[] args);
        Task LogInfo(string message);
        Task LogWarning(string message, params object[] args);
        Task LogWarning(string message, Exception exception);
        Task LogWarning(string message);
    }
}
