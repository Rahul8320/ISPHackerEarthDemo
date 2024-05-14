using ISPHackerEarth.Application.Services;
using ISPHackerEarth.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ISPHackerEarth.Application;

public static class Extensions
{
    public static IServiceCollection AddApplications(this IServiceCollection services)
    {
        services.AddScoped<IISPService, ISPService>();

        return services;
    }
}
