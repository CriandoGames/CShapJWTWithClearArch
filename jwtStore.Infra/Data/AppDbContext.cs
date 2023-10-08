using jwtStore.core.Context.AccountContext.Entities;
using jwtStore.Infra.Context.AccountContext.Mappings;
using Microsoft.EntityFrameworkCore;


namespace jwtStore.Infra.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
