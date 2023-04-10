using Comfy.Application.Interfaces;
using Comfy.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace Comfy.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ComfyDbContextConnection")
                ?? throw new InvalidOperationException("Connection string 'ComfyDbContextConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(config => 
            {
                config.UseMySql(connectionString, new MySqlServerVersion(new Version(11, 0, 1)));
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

            services.AddIdentity<User, ApplicationRole>(config => UseTestingIdentityConfig(config))
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
    }
}
