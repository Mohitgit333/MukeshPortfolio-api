using System.ComponentModel.DataAnnotations;

namespace MukeshPortfolio.API.DTOs
{
    public class SkillUpdateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string ProficiencyLevel { get; set; } = string.Empty;

        [Range(0, 50)]
        public int? YearsOfExperience { get; set; }

        [Range(0, 1000)]
        public int DisplayOrder { get; set; }
    }
}
