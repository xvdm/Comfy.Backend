using Comfy.Application;
using Comfy.Application.Common.Mappings;
using Comfy.Application.Interfaces;
using Comfy.Persistence;
using Comfy.WebApi.Middlewares;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.IO.Compression;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

IConfiguration configuration = builder.Configuration;
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
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
});

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
app.UseAuthentication();;
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

app.Run();