namespace ISPHackerEarth.Domain.Common.Services;

public interface ILoggerService
{
    void LogInformation(string message, Guid? ispId = null, string? ispName = null);
    void LogWarning(string message, Guid? ispId = null, string? ispName = null);
    void LogError(string message, Guid? ispId = null, string? ispName = null, Exception? exception = null);
}
