using jwtStore.core.Context.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace jwtStore.Infra.Context.AccountContext.Mappings
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {


        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            // Primary Key
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("VARCHAR").HasMaxLength(120).IsRequired(true);
            builder.Property(x => x.Image).HasColumnName("Image").HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(true);
            builder.OwnsOne(x => x.Email).Property(x => x.Value).HasColumnName("Email").HasColumnType("VARCHAR").HasMaxLength(255).IsRequired(true);
            builder.OwnsOne(x => x.Email).OwnsOne(x => x.Verification).Property(x => x.Code).HasColumnName("EmailVerificationCode")
            .IsRequired(true);

            builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.ExpiresAt)
            .HasColumnName("EmailVerificationExpiresAt")
            .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.Verification)
                .Property(x => x.VerifiedAt)
                .HasColumnName("EmailVerificationVerifiedAt")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.Verification)
                .Ignore(x => x.IsActive);

            builder.OwnsOne(x => x.Password)
                .Property(x => x.Hash)
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.OwnsOne(x => x.Password)
                .Property(x => x.ResetCode)
                .HasColumnName("PasswordResetCode")
                .IsRequired();

            // Relationships
            builder.HasMany(r => r.Roles)
                   .WithMany(u => u.Users)
                   .UsingEntity<Dictionary<string, object>>(
                                      "UserRoles",
                       roles => roles.HasOne<Role>()
                       .WithMany().HasForeignKey("RoleId")
                       .OnDelete(DeleteBehavior.Cascade),

                        user => user.HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade));

        }
    }
}
