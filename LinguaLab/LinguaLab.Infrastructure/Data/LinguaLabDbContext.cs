using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinguaLab.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LinguaLab.Infrastructure.Data
{
    public class LinguaLabDbContext : DbContext
    {
        public LinguaLabDbContext(DbContextOptions<LinguaLabDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WordProgress> WordProgresses { get; set; }
        public DbSet<ReviewLog> ReviewLogs { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(eb =>
            {
                eb.HasMany(c => c.Words)
                .WithOne(w => w.Category)
                .HasForeignKey(w => w.CategoryId);
            });

            modelBuilder.Entity<User>(eb =>
            {
                eb.HasMany<Word>()
                .WithOne(w => w.CreatedBy)
                .HasForeignKey(w => w.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<WordProgress>(eb =>
            {
                eb.HasKey(wp => new { wp.UserId, wp.WordId });

                eb.HasOne(wp => wp.User)
                .WithMany()
                .HasForeignKey(wp => wp.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                eb.HasOne(wp => wp.Word)
                .WithMany()
                .HasForeignKey(wp => wp.WordId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ReviewLog>(eb =>
            {
                eb.HasOne(rl => rl.User)
                .WithMany()
                .HasForeignKey(rl => rl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                eb.HasOne(rl => rl.Word)
                .WithMany()
                .HasForeignKey(rl => rl.WordId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserAchievement>(eb =>
            {
                eb.HasKey(ua => new { ua.UserId, ua.AchievementId });

                eb.HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId);

                eb.HasOne(ua => ua.Achievement)
                .WithMany()
                .HasForeignKey(ua => ua.AchievementId);
            });

        }
    }
}
