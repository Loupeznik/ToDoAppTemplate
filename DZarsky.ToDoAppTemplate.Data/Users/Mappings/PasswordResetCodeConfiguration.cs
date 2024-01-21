using DZarsky.ToDoAppTemplate.Data.Common.Constants;
using DZarsky.ToDoAppTemplate.Data.Common.Mappings;
using DZarsky.ToDoAppTemplate.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DZarsky.ToDoAppTemplate.Data.Users.Mappings;

public sealed class PasswordResetCodeConfiguration()
    : BaseEntityConfiguration<PasswordResetCode>(Tables.PasswordResetCodes)
{
    public override void Configure(EntityTypeBuilder<PasswordResetCode> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Code)
               .IsRequired()
               .HasMaxLength(8);

        builder
            .Property(x => x.ExpirationDate)
            .IsRequired();
    }
}
