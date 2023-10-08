using jwtStore.core.Context.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace jwtStore.Infra.Context.AccountContext.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(x => x.Id); 
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .HasColumnType("VARCHAR")
                .IsRequired(true);
        }
    }
}
