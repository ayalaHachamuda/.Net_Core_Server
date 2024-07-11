using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
namespace Models.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {
        }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Vote> Votes { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Recipe)
                .WithMany(r => r.Votes)
                .HasForeignKey(v => v.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            // הגדרת הקשר בין User ל-Vote דרך Recipe
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Votes)
                .WithOne(v => v.Recipe)
                .HasForeignKey(v => v.RecipeId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Votes)
                .WithOne()
                .HasForeignKey(v => v.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
