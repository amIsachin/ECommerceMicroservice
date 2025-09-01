using Microsoft.Extensions.Configuration;
using Auth.Application;
using Auth.Infrastructure;
using Auth.Core;

namespace Auth.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDI().AddInfrastructureDI(configuration).AddCoreDI(configuration);

        return services;
    }
}
