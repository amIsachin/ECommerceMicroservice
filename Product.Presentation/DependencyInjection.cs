using Product.Application;
using Product.Infrastructure;
using Core;

namespace Product.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationDI().AddInfrastructureDI().AddCoreDI(configuration);

        return services;
    }
}
