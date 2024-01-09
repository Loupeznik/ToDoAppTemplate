using DZarsky.ToDoAppTemplate.Core.Auth.Services;
using DZarsky.ToDoAppTemplate.Core.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace DZarsky.ToDoAppTemplate.Core.Infrastructure.Configuration;

public static class CoreConfigurationExtensions
{
    public static IServiceCollection AddToDoCore(this IServiceCollection services)
    {
        services.AddScoped<AuthManager>();
        services.AddScoped<PasswordValidator>();
        services.AddScoped<PasswordHasher>();

        return services;
    }
}
