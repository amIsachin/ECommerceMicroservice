﻿using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application;

public static class DependencyInjection
{   
    public static IServiceCollection AddApplicationDI(this IServiceCollection services)
    {
        return services;
    }
}
