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
            .WriteTo.File(path: "Logs/info-.json", restrictedToMinimumLevel: LogEventLevel.Information)
            .WriteTo.File(path: "Logs/error-.json", restrictedToMinimumLevel: LogEventLevel.Error)
            .WriteTo.File(path: "Logs/request-.json", restrictedToMinimumLevel: LogEventLevel.Debug);

        _logger = loggerConfiguration.CreateLogger();
    }
    public void LogError(LogData logData, Exception? exception = null)
    {
        logData.Message = $"Error :: {logData.Message}";
        var jsonData = JsonSerializer.Serialize(logData);
        _logger.Error(jsonData, exception);
    }

    public void LogInformation(LogData logData)
    {
        logData.Message = $"Information :: {logData.Message}";
        var jsonData = JsonSerializer.Serialize(logData);
        _logger.Information(jsonData);
    }

    public void LogWarning(LogData logData)
    {
        logData.Message = $"Warning :: {logData.Message}";
        var jsonData = JsonSerializer.Serialize(logData);
        _logger.Warning(jsonData);
    }
}
