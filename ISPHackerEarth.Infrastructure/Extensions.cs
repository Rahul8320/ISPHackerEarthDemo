using ISPHackerEarth.Domain.Common.Services;
using ISPHackerEarth.Domain.Repositories;
using ISPHackerEarth.Infrastructure.Repositories;
using ISPHackerEarth.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ISPHackerEarth.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructures(this IServiceCollection services)
    {
        services.AddScoped<IISPRepository, ISPRepository>();
        services.AddSingleton<ILoggerService, LoggerService>();

        return services;
    }
}
