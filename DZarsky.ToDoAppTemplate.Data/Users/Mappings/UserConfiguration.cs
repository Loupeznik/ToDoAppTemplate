using DZarsky.ToDoAppTemplate.Data.Common.Constants;
using DZarsky.ToDoAppTemplate.Data.Common.Mappings;
using DZarsky.ToDoAppTemplate.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DZarsky.ToDoAppTemplate.Data.Users.Mappings;

public sealed class UserConfiguration() : BaseEntityConfiguration<User>(Tables.Users)
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Login)
               .IsRequired()
               .HasMaxLength(60);

        builder.Property(x => x.Password)
               .IsRequired();

        builder.Property(x => x.Email)
               .HasMaxLength(128);
    }
}
