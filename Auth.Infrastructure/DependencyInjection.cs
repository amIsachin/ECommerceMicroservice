using Auth.Application.Interfaces;
using Auth.Core.Entities;
using Auth.Core.Options;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
    {
        // -----------------------------
        // ✅ Database
        // -----------------------------
        services.AddDbContext<AuthDbContext>((provider, options) =>
        {
            var connectionString = provider
                .GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>()
                .Value.DefaultConnection;

            options.UseSqlServer(connectionString);
        });

        // -----------------------------
        // ✅ Identity Configuration
        // -----------------------------
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            // Password rules
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;

            // Lockout rules
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;

            // User account rules
            options.User.RequireUniqueEmail = true;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AuthDbContext>()
        .AddDefaultTokenProviders();

        // -----------------------------
        // ✅ JWT Settings
        // -----------------------------
        var jwtSection = configuration.GetSection("JwtSettings");
        services.Configure<JWTSettings>(jwtSection);

        var jwtSettings = jwtSection.Get<JWTSettings>();

        // -----------------------------
        // ✅ Authentication & JWT
        // -----------------------------
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromMinutes(2),
                ValidIssuer = jwtSettings!.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };
        });

        // -----------------------------
        // ✅ Identity Managers
        // -----------------------------
        services.AddScoped<UserManager<ApplicationUser>>();
        services.AddScoped<RoleManager<IdentityRole>>();

        // -----------------------------
        // ✅ Custom Services
        // -----------------------------
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}
