using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Comfy.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Comfy.Application.Services.JwtAccessToken;
using Comfy.Application.Services.RefreshTokens;

namespace Comfy.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(JwtValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

        services.AddTransient<ICreateJwtAccessTokenService, CreateJwtAccessTokenService>();
        services.AddTransient<ICreateRefreshTokenService, CreateRefreshTokenService>();

        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("uk");

        return services;
    }
}