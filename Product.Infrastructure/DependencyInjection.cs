using Core.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Product.Application.Commands;
using Product.Application.Interfaces;
using Product.Application.Quesries;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repositories;

namespace Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>((provider, options) =>
        {
            options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection);
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<AddProductCommandHandler>();
        services.AddScoped<GetAllProductQueryHandler>();

        return services;
    }

}