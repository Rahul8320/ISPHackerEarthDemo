using ISPHackerEarth.Domain.Common.Model;

namespace ISPHackerEarth.Domain.Common.Service;

public interface ILoggerService
{
    void LogInformation(LogData logData);
    void LogWarning(LogData logData);
    void LogError(LogData logData, Exception? exception = null);
}
