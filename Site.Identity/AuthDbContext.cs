using Site.Data.Entities.Oauth;
using System.Data.Entity;

namespace Site.Identity
{
    public partial class AuthDbContext : DbContext
    {
        public AuthDbContext()
            : base("oauth")
        {
            Database.SetInitializer<AuthDbContext>(null);
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("oauth.account");
            modelBuilder.Entity<Role>().ToTable("oauth.role");
        }

        public DbSet<Account> Accounts { get; set; }
    }
}