﻿using System.Security.Claims;
using Comfy.Application.Interfaces;
using Comfy.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Comfy.Application.Common.Helpers;

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

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                };
            });


        services.AddAuthorization(options =>
        {
            options.AddPolicy(RoleNames.Administrator, policyBuilder =>
            {
                policyBuilder.RequireAssertion(x =>
                    x.User.IsInRole(RoleNames.Administrator) ||
                    x.User.IsInRole(RoleNames.SeniorAdministrator) ||
                    x.User.IsInRole(RoleNames.Owner));
            });
            options.AddPolicy(RoleNames.User, policyBuilder =>
            {
                policyBuilder.RequireAssertion(x =>
                    x.User.IsInRole(RoleNames.User) ||
                    x.User.IsInRole(RoleNames.Administrator) ||
                    x.User.IsInRole(RoleNames.SeniorAdministrator) ||
                    x.User.IsInRole(RoleNames.Owner));
            });
        });

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