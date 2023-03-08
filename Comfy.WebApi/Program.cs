using Comfy.Application.Common.Mappings;
using System.Reflection;
using Comfy.Application.Interfaces;
using Comfy.Application;
using Comfy.Persistence;
using Comfy.Persistence.Converters;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IComfyDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new CharacteristicNameConverter());
    options.JsonSerializerOptions.Converters.Add(new CharacteristicDictionaryConverter());
});


//todo: разрешить только для фронтенда
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});


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

app.Run();




//static void UseTestingIdentityConfig(IdentityOptions config)
//{
//    config.SignIn.RequireConfirmedAccount = false;
//    config.Password.RequireNonAlphanumeric = false;
//    config.Password.RequireUppercase = false;
//    config.Password.RequireLowercase = true;
//    config.Password.RequiredLength = 3;
//    config.Password.RequireDigit = false;
//}

//static void UseProductionIdentityConfig(IdentityOptions config)
//{
//    config.SignIn.RequireConfirmedAccount = false;
//    config.Password.RequireNonAlphanumeric = true;
//    config.Password.RequireUppercase = true;
//    config.Password.RequireLowercase = true;
//    config.Password.RequiredLength = 10;
//    config.Password.RequireDigit = true;
//}