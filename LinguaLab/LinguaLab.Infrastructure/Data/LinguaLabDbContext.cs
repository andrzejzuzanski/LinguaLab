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
            :base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Category> Categories { get; set; }

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
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
