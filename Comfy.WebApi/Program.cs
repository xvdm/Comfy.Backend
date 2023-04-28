using Comfy.Application.Common.Mappings;
using System.Reflection;
using System.Text.Json.Serialization;
using Comfy.Application.Interfaces;
using Comfy.Application;
using Comfy.Persistence;
using Comfy.Persistence.Converters;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.Converters.Add(new CharacteristicNameConverter());
    options.JsonSerializerOptions.Converters.Add(new CharacteristicDictionaryConverter());
});


//todo: allow only for frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

builder.Services.AddSwaggerGen();

builder.Services.AddStartupTask<WarmupServicesStartupTask>().TryAddSingleton(builder.Services);

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseAuthentication();;
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStartupTask<T>(this IServiceCollection services)
        where T : class, IStartupTask
        => services.AddTransient<IStartupTask, T>();
}

public interface IStartupTask
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}


public class WarmupServicesStartupTask : IStartupTask
{
    private readonly IServiceCollection _services;
    private readonly IServiceProvider _provider;
    public WarmupServicesStartupTask(IServiceCollection services, IServiceProvider provider)
    {
        _services = services;
        _provider = provider;
    }

    public Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using (var scope = _provider.CreateScope())
        {
            foreach (var singleton in GetServices(_services))
            {
                scope.ServiceProvider.GetServices(singleton);
            }
        }

        return Task.CompletedTask;
    }

    static IEnumerable<Type> GetServices(IServiceCollection services)
    {
        return services
            .Where(descriptor => descriptor.ImplementationType != typeof(WarmupServicesStartupTask))
            .Where(descriptor => descriptor.ServiceType.ContainsGenericParameters == false)
            .Select(descriptor => descriptor.ServiceType)
            .Distinct();
    }
}