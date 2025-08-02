using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Core.Options;

namespace Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.sectionName));

        return services;

    }
}