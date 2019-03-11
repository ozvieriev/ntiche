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
            modelBuilder.Entity<Data.Entities.Oauth.Account>().ToTable("oauth.account");
            modelBuilder.Entity<Data.Entities.Oauth.Role>().ToTable("oauth.role");


            modelBuilder.Entity<Data.Entities.Test.Feedback>().ToTable("test.feedback");
        }

        public DbSet<Data.Entities.Oauth.Account> Accounts { get; set; }

        public DbSet<Data.Entities.Test.Feedback> Feedbacks { get; set; }
    }
}