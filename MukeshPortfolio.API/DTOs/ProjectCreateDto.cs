using System.ComponentModel.DataAnnotations;

namespace MukeshPortfolio.API.DTOs
{
    public class ProjectCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string Technologies { get; set; } = string.Empty;

        [Url(ErrorMessage = "GitHubUrl must be a valid URL")]
        public string? GitHubUrl { get; set; }

        [Url]
        public string? LiveDemoUrl { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

    }
}
