using Comfy.Application;
using Comfy.Application.Common.Helpers;
using Comfy.Application.Common.Mappings;
using Comfy.Application.Interfaces;
using Comfy.Persistence;
using Comfy.WebApi.Middlewares;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Compression;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("ComfyDbContextConnection"))
    .AddRedis(builder.Configuration.GetConnectionString("Redis"));

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

IConfiguration configuration = builder.Configuration;
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = false;
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplication(configuration);
builder.Services.AddPersistence(configuration);

builder.Services.AddControllers();

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

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Enter a valid token",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Description = "JwtValidation tag - request must contain 'Authorization' header with JWT access token.<br>" +
                      "The header should look like this: 'Bearer jwt', where 'Bearer' is a constant and 'jwt' is the actual jwt token." +
                      "<br> <br>" +

                      "If the response contains 'Token-Expired' header with value 'true', it means that JWT access token is expired. You need to refresh it and make the request again." +
                      "<br> <br>" +

                      "Registration flow: <br>" +
                      "1. Send confirmation code to email (Email/sendRegistrationEmail) <br>" +
                      "2. Confirm email (Email/confirmEmail) <br>" +
                      "3. Register a user (Auth/register)" +
                      "<br><br>" +

                      "Sign in flow:<br/>" +
                      "1. Check credentials (Auth/checkCredentialsAndTwoFactor)<br>" +
                      "2. If two-factor is disabled - sign in with (Auth/signIn-Password)<br>" +
                      "2.1. If two-factor is enabled:<br>" +
                      "3. Verify authentication code with (TwoFactorAuth/verifyCode)<br>" +
                      "4. If code is verified - sign in with (Auth/signIn-Password)" +
                      "<br><br>" +

                      "Registration via Google:<br/>" +
                      "1. Register using Google Id Token that was obtained on the frontend (Auth/signIn-Google)" +
                      "<br><br>" +

                      "Sign in via Google:<br/>" +
                      "1. Sign in using Google Id Token that was obtained on the frontend (Auth/signIn-Google)" +
                      "<br><br>" +

                      "Enable two-factor authentication:<br>" +
                      "1. Get auth setup info and display to the user (TwoFactorAuth/getAuthSetupInfo)" +
                      "2. Verify authentication code (TwoFactorAuth/verifyCode)<br>" +
                      "3. If code is verified, than enable two-factor auth (TwoFactorAuth/enableTwoFactor)"+
                      "<br><br>" +

                      "Disable two-factor authentication:<br>" +
                      "1. Verify authentication code (TwoFactorAuth/verifyCode)<br>" +
                      "2. If code is verified, than disable two-factor auth (TwoFactorAuth/disableTwoFactor)"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthentication();
app.UseAuthorization();

app.UseResponseCompression();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.DisplayRequestDuration();
    });
}

app.MapHealthChecks("/api/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).RequireAuthorization(policyNames: new[] { RoleNames.Administrator });

app.Run();