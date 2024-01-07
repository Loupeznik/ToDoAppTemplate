using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;

namespace DZarsky.ToDoAppTemplate.Api.Infrastructure.Configuration;

internal static class ApiConfigurationExtensions
{
    internal static IApplicationBuilder ConfigureToDoApi(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseFastEndpoints(x => { x.Endpoints.RoutePrefix = "api"; });
        app.UseSwaggerGen();

        return app;
    }

    internal static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.SwaggerDocument();
        services
            .AddFastEndpoints()
            .AddJWTBearerAuth(string.Empty) // todo: take jwk from configuration
            .AddAuthorization(); 

        return services;
    }
}
