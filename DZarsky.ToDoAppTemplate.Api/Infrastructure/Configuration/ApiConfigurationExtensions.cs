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
        services.SwaggerDocument();
        services
            .AddFastEndpoints()
            .AddJWTBearerAuth(configuration.GetSection("Api").GetValue<string>("SigningKey") ??
                              throw new InvalidConfigurationException("No signing key provider"))
            .AddAuthorization();

        return services;
    }
}
