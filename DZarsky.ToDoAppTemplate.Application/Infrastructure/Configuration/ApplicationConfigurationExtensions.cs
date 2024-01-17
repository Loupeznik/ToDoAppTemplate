using DZarsky.ToDoAppTemplate.Application.Infrastructure.MediatR.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DZarsky.ToDoAppTemplate.Application.Infrastructure.Configuration;

public static class ApplicationConfigurationExtensions
{
    public static IServiceCollection AddToDoApplication(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(ApplicationConfigurationExtensions).Assembly);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestWithUserIdPipelineBehaviour<,>));

        return services;
    }
}
