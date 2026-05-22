using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JobBoard.Domain.Entities;


namespace JobBoard.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<User> Users => Set<User>();
        public DbSet<JobBoard.Domain.Entities.Application> Applications => Set<JobBoard.Domain.Entities.Application>();
        public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .Property(j => j.Salary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Job>()
                .HasOne(j => j.CreatedBy)
                .WithMany(u => u.Jobs)
                .HasForeignKey(j => j.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobBoard.Domain.Entities.Application>()
                .HasOne(a => a.User)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.CandidateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobBoard.Domain.Entities.Application>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PasswordResetToken>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
