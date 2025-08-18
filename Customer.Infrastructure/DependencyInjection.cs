using Customer.Application.Interfaces;
using Customer.Core.Options;
using Customer.Infrastructure.Data;
using Customer.Infrastructure.Reositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Customer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
    {
        services.AddDbContext<CustomerDbContext>((provider, options) =>
        {
            options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection);
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        return services;
    }
}
