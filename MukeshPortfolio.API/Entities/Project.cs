using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MukeshPortfolio.API.Models.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(300)]
        public string Technologies { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? GitHubUrl { get; set; }

        [MaxLength(500)]
        public string? LiveDemoUrl { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        // =========================================================
        // Foreign Key → Users table
        // =========================================================
        [Required]
        public int UserId { get; set; }

        // Navigation property — EF Core isko use karta hai relationship ke liye
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}