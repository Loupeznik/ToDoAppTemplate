using ToDoAppTemplate.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoAppTemplate.Data.Common.Constants;
using ToDoAppTemplate.Data.Common.Mappings;

namespace ToDoAppTemplate.Data.Users.Mappings;

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

        builder
            .Property(x => x.IsBlocked)
            .HasDefaultValue(false);

        builder.HasMany(x => x.PasswordResetCodes)
               .WithOne(x => x.User)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
