using Microsoft.Extensions.DependencyInjection;

namespace DZarsky.ToDoAppTemplate.Application.Infrastructure.Configuration;

public static class ApplicationConfigurationExtensions
{
    public static IServiceCollection AddToDoApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ApplicationConfigurationExtensions).Assembly);
        });

        return services;
    }
}
