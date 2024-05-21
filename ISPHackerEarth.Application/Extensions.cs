using Microsoft.Extensions.DependencyInjection;

namespace ISPHackerEarth.Application;

public static class Extensions
{
    public static IServiceCollection AddApplications(this IServiceCollection services)
    {
        // Injecting MediatR to our DI
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(Extensions).Assembly));

        return services;
    }
}
