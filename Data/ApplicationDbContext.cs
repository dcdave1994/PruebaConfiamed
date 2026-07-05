using Microsoft.EntityFrameworkCore;
using PruebaConfiamed.Entities;

namespace PruebaConfiamed.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<WorkItem> WorkItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<WorkItem>()
                .HasOne(w => w.AppUser)
                .WithMany()
                .HasForeignKey(w => w.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}