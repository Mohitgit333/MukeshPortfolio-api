namespace MukeshPortfolio.API.DTOs
{
    public class ExperienceDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string? Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrent { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Technologies { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
