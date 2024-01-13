using DZarsky.ToDoAppTemplate.Api.Infrastructure.Security;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.IdentityModel.Protocols.Configuration;

namespace DZarsky.ToDoAppTemplate.Api.Infrastructure.Configuration;

internal static class ApiConfigurationExtensions
{
    internal static IApplicationBuilder ConfigureToDoApi(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseFastEndpoints(x =>
        {
            x.Endpoints.RoutePrefix = "api";
        });
        app.UseSwaggerGen();

        return app;
    }

    internal static IServiceCollection AddEndpoints(this IServiceCollection services, IConfiguration configuration)
    {
        var securityConfiguration = configuration.GetSection("Api").GetSection("Auth").Get<AuthConfiguration>() ??
                                    throw new InvalidConfigurationException("No auth configuration found");

        services.AddSingleton(securityConfiguration);

        services.SwaggerDocument();
        services
            .AddFastEndpoints()
            .AddJWTBearerAuth(!string.IsNullOrWhiteSpace(securityConfiguration.SigningKey)
                ? securityConfiguration.SigningKey
                : throw new InvalidConfigurationException("No signing key found"))
            .AddAuthorization();

        services.AddScoped<TokenGenerator>();

        return services;
    }
}
