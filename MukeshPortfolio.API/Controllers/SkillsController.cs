using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MukeshPortfolio.API.DTOs;

namespace MukeshPortfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private static readonly List<SkillDto> _skills = new()
        {
            new SkillDto
            {
                Id = 1,
                Name = "C#",
                Category = "Backend",
                ProficiencyLevel = "Expert",
                YearsOfExperience = 7,
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow
            },
            new SkillDto
            {
                Id = 2,
                Name = "ASP.NET Core",
                Category = "Backend",
                ProficiencyLevel = "Advanced",
                YearsOfExperience = 5,
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow
            },
            new SkillDto
            {
                Id = 3,
                Name = "React",
                Category = "Frontend",
                ProficiencyLevel = "Intermediate",
                YearsOfExperience = 1,
                DisplayOrder = 3,
                CreatedAt = DateTime.UtcNow
            },
            new SkillDto
            {
                Id = 4,
                Name = "SQL Server",
                Category = "Database",
                ProficiencyLevel = "Advanced",
                YearsOfExperience = 6,
                DisplayOrder = 4,
                CreatedAt = DateTime.UtcNow
            }
        };

        private static int _nextId = 5;

        [HttpGet]
        public ActionResult<IEnumerable<SkillDto>> GetAll()
        {
            var sorted = _skills.OrderBy(s => s.DisplayOrder).ToList();
            return Ok(sorted);
        }

        [HttpGet("{id:int}")]
        public ActionResult<SkillDto> GetById(int id)
        {
            var skill = _skills.FirstOrDefault(s => s.Id == id);
            if (skill == null)
                return NotFound(new { message = $"Skill with id {id} not found" });

            return Ok(skill);
        }

        [HttpPost]
        public ActionResult<SkillDto> Create([FromBody] SkillCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newSkill = new SkillDto
            {
                Id = _nextId++,
                Name = dto.Name,
                Category = dto.Category,
                ProficiencyLevel = dto.ProficiencyLevel,
                YearsOfExperience = dto.YearsOfExperience,
                DisplayOrder = dto.DisplayOrder,
                CreatedAt = DateTime.UtcNow
            };

            _skills.Add(newSkill);

            return CreatedAtAction(nameof(GetById), new { id = newSkill.Id }, newSkill);
        }

        [HttpPut("{id:int}")]
        public ActionResult<SkillDto> Update(int id, [FromBody] SkillUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var skill = _skills.FirstOrDefault(s => s.Id == id);
            if (skill == null)
                return NotFound(new { message = $"Skill with id {id} not found" });

            skill.Name = dto.Name;
            skill.Category = dto.Category;
            skill.ProficiencyLevel = dto.ProficiencyLevel;
            skill.YearsOfExperience = dto.YearsOfExperience;
            skill.DisplayOrder = dto.DisplayOrder;

            return Ok(skill);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var skill = _skills.FirstOrDefault(s => s.Id == id);
            if (skill == null)
                return NotFound(new { message = $"Skill with id {id} not found" });

            _skills.Remove(skill);
            return NoContent();
        }


    }
}
