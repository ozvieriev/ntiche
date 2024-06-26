﻿using System.Data.Entity;

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

            modelBuilder.Entity<Data.Entities.Test.Exam>().ToTable("test.exam");
            modelBuilder.Entity<Data.Entities.Test.ExamQuestion>().ToTable("test.examQuestion");
            modelBuilder.Entity<Data.Entities.Test.ExamResult>().ToTable("test.examResult");
            modelBuilder.Entity<Data.Entities.Test.Feedback>().ToTable("test.feedback");
        }

        public DbSet<Data.Entities.Oauth.Account> Accounts { get; set; }

        public DbSet<Data.Entities.Test.Exam> Exams { get; set; }
        public DbSet<Data.Entities.Test.ExamQuestion> ExamQuestions { get; set; }
        public DbSet<Data.Entities.Test.ExamResult> ExamResults { get; set; }
        public DbSet<Data.Entities.Test.Feedback> Feedbacks { get; set; }
    }
}