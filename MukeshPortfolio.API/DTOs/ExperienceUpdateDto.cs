using System.ComponentModel.DataAnnotations;

namespace MukeshPortfolio.API.DTOs
{
    public class ExperienceUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Location { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Technologies { get; set; } = string.Empty;

    }
}
