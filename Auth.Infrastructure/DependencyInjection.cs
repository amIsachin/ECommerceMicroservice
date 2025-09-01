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
        services.AddDbContext<AuthDbContext>((provider, options) =>
        {
            options.UseSqlServer(provider.GetRequiredService<IOptionsSnapshot<ConnectionStringOptions>>().Value.DefaultConnection);
        });

        // ✅ Persistence + Role/User management only
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            // ---- Password rules ----
            options.Password.RequireDigit = true;         // Password must include at least one number
            options.Password.RequireUppercase = false;    // Uppercase letters not required
            options.Password.RequiredLength = 6;          // Minimum length = 6 characters

            // ---- Lockout rules ----
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  // Lockout duration if too many failed logins
            options.Lockout.MaxFailedAccessAttempts = 5;   // After 5 failed attempts → account locked

            // ---- User account rules ----
            options.User.RequireUniqueEmail = true;       // Each user must have a unique email
        })
        // Adds **role support** (e.g., Admin, User, Manager, etc.)
        .AddRoles<IdentityRole>()
        // Configures Identity to use **EF Core persistence** with your `AuthDbContext`.
        // This means users, roles, claims, logins, etc. will be stored in the database.
        .AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();

        // ✅ JWT Configuration
        services.Configure<JWTSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            // ✅ Resolve settings through IOptions at runtime (same as JwtService)
            var sp = services.BuildServiceProvider();
            var jwtSettings =  sp.GetRequiredService<IOptions<JWTSettings>>().Value;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromMinutes(2),
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };
        });


        services.AddScoped<UserManager<ApplicationUser>>();
        // services.AddScoped<SignInManager<ApplicationUser>>();  // ✅ Now works

        services.AddScoped<RoleManager<IdentityRole>>();

        // ✅ Custom Services
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}