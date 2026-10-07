using Microsoft.EntityFrameworkCore;
using MukeshPortfolio.API.Models.Entities;

namespace MukeshPortfolio.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // =========================================================
        // DbSets
        // =========================================================
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Experience> Experiences { get; set; }

        // =========================================================
        // OnModelCreating — Fluent API configuration
        // =========================================================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------- User configuration --------
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                entity.Property(u => u.Role).IsRequired().HasMaxLength(20);

                // Unique indexes — duplicate email/username prevent karte hain
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Username).IsUnique();
            });

            // -------- Project configuration --------
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Title).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Description).IsRequired().HasMaxLength(1000);
                entity.Property(p => p.Technologies).IsRequired().HasMaxLength(300);
                entity.Property(p => p.GitHubUrl).HasMaxLength(500);
                entity.Property(p => p.LiveDemoUrl).HasMaxLength(500);
                entity.Property(p => p.ImageUrl).HasMaxLength(500);

                // -------- Relationship: Project → User (Many-to-One) --------
                entity.HasOne(p => p.User)
                      .WithMany(u => u.Projects)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);   // User delete → uske projects bhi delete

                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // -------- Skill configuration --------
            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Category).IsRequired().HasMaxLength(50);
                entity.Property(s => s.ProficiencyLevel).IsRequired().HasMaxLength(30);

                entity.HasIndex(s => s.Category)
                      .HasDatabaseName("IX_Skills_Category");

                // -------- Relationship: Skill → User --------
                entity.HasOne(s => s.User)
                      .WithMany(u => u.Skills)
                      .HasForeignKey(s => s.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(s => s.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // -------- Experience configuration --------
            modelBuilder.Entity<Experience>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Location).HasMaxLength(100);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);
                entity.Property(e => e.Technologies).IsRequired().HasMaxLength(500);

                // -------- Relationship: Experience → User --------
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Experiences)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // =========================================================
            // Seed Data — default admin user (baad mein password hash karenge)
            // Abhi ke liye dummy hash — JWT phase mein proper hashing add karenge
            // =========================================================
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "mukesh",
                    Email = "Joshi.mohit94@hotmail.com",
                    PasswordHash = "PLACEHOLDER_HASH_UPDATE_LATER",  // Phase 7 mein proper hash
                    Role = "Admin",
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );
        }
    }
}