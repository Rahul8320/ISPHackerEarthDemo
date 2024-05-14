using ISPHackerEarth.Domain.Repositories;
using ISPHackerEarth.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ISPHackerEarth.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructures(this IServiceCollection services)
    {
        services.AddScoped<IISPRepository, ISPRepository>();

        return services;
    }
}
