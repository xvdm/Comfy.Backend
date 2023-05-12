﻿using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Comfy.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ComfyDbContextConnection")
            ?? throw new InvalidOperationException("Connection string 'ComfyDbContextConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(config => 
        {
            config.UseNpgsql(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

        services.AddIdentity<User, ApplicationRole>(UseTestingIdentityConfig)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }

    private static void UseTestingIdentityConfig(IdentityOptions config)
    {
        config.SignIn.RequireConfirmedAccount = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
        config.Password.RequireLowercase = true;
        config.Password.RequiredLength = 3;
        config.Password.RequireDigit = false;
    }

    private static void UseProductionIdentityConfig(IdentityOptions config)
    {
        config.SignIn.RequireConfirmedAccount = false;
        config.Password.RequireNonAlphanumeric = true;
        config.Password.RequireUppercase = true;
        config.Password.RequireLowercase = true;
        config.Password.RequiredLength = 10;
        config.Password.RequireDigit = true;
    }
}