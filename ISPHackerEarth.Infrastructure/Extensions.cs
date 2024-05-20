using ISPHackerEarth.Domain.Common.Service;
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
        services.AddScoped<ILoggerService, LoggerService>();

        return services;
    }
}
