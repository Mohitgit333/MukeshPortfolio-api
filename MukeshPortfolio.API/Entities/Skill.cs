using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MukeshPortfolio.API.Models.Entities
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string ProficiencyLevel { get; set; } = string.Empty;

        public int? YearsOfExperience { get; set; }

        [Range(0, 1000)]
        public int DisplayOrder { get; set; }

        // =========================================================
        // Foreign Key → Users table
        // =========================================================
        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}