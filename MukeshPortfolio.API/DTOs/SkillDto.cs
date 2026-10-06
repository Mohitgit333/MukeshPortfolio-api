namespace MukeshPortfolio.API.DTOs
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ProficiencyLevel { get; set; } = string.Empty;
        public int? YearsOfExperience { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
