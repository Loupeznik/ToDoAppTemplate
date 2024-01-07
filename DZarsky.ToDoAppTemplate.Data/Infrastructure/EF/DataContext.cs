using Microsoft.EntityFrameworkCore;

namespace DZarsky.ToDoAppTemplate.Data.Infrastructure.EF;

public sealed class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
