using System.ComponentModel.DataAnnotations;

namespace MukeshPortfolio.API.Models.Entities
{
    /// <summary>
    /// User entity — portfolio admin / owner represent karta hai.
    /// Abhi single user hai (Mukesh), lekin design multi-user ready hai.
    /// Future mein JWT auth ke saath use hoga.
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "Admin"; // Admin, User

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // =========================================================
        // Navigation Properties — yeh FK relationship ko represent karti hain
        // Ek User ke multiple Projects, Skills, Experiences ho sakte hain
        // =========================================================
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
    }
}