using System.Text.Json;
using ISPHackerEarth.Domain.Common.Model;
using ISPHackerEarth.Domain.Common.Service;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace ISPHackerEarth.Infrastructure.Service;

public class LoggerService : ILoggerService
{
    private readonly Serilog.Core.Logger _logger;

    public LoggerService(IConfiguration configuration)
    {
        var loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.File(path: "Logs/info-.log", restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
            .WriteTo.File(path: "Logs/error-.log", restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true);

        _logger = loggerConfiguration.CreateLogger();
    }
    public void LogError(string message, Guid? ispId = null, string? ispName = null, Exception? exception = null)
    {
        message = $"Error :: {message}";
        var logData = new LogData(message, ispId, ispName);
        var jsonData = JsonSerializer.Serialize(logData);
        _logger.Error(jsonData, exception);
    }

    public void LogInformation(string message, Guid? ispId = null, string? ispName = null)
    {
        message = $"Information :: {message}";
        var logData = new LogData(message, ispId, ispName);
        var jsonData = JsonSerializer.Serialize(logData);
        _logger.Information(jsonData);
    }

    public void LogWarning(string message, Guid? ispId = null, string? ispName = null)
    {
        message = $"Warning :: {message}";
        var logData = new LogData(message, ispId, ispName);
        var jsonData = JsonSerializer.Serialize(logData);
        _logger.Warning(jsonData);
    }
}
