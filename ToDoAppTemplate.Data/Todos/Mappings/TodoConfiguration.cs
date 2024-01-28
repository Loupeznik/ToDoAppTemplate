using ToDoAppTemplate.Domain.Todos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoAppTemplate.Data.Common.Constants;
using ToDoAppTemplate.Data.Common.Mappings;

namespace ToDoAppTemplate.Data.Todos.Mappings;

public sealed class TodoConfiguration() : BaseEntityConfiguration<Todo>(Tables.Todos)
{
    public override void Configure(EntityTypeBuilder<Todo> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title)
               .IsRequired()
               .HasMaxLength(255);

        builder.Property(x => x.Description)
               .HasMaxLength(2048);
    }
}
