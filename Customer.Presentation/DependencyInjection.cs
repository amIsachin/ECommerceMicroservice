namespace Customer.Presentation;
using Customer.Application;
using Customer.Infrastructure;
using Customer.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDI().AddInfrastructureDI().AddCoreDI(configuration);

        return services;
    }
}