using Site.Data.Entities.Oauth;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

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
        }

        public DbSet<Account> Accounts { get; set; }
    }
}