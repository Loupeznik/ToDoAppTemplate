using DZarsky.ToDoAppTemplate.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DZarsky.ToDoAppTemplate.Data.Common.Mappings;

public abstract class BaseEntityConfiguration<TEntity>(string tableName) : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    private const string Now = "NOW()";

    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(tableName);
        
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.DateCreated)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql(Now);
        
        builder.Property(x => x.DateUpdated)
               .ValueGeneratedOnUpdate()
               .HasDefaultValueSql(Now);

        builder
            .HasQueryFilter(x => !x.IsDeleted);
    }
}
