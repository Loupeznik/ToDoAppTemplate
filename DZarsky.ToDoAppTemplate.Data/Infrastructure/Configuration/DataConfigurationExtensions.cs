using DZarsky.ToDoAppTemplate.Data.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DZarsky.ToDoAppTemplate.Data.Infrastructure.Configuration;

public static class DataConfigurationExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("db"),
                                  x => x.MigrationsAssembly(typeof(DataContext).Assembly.GetName().Name))
                              .UseSnakeCaseNamingConvention());

        return services;
    }
}
