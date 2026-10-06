using Microsoft.AspNetCore.Mvc;
using MukeshPortfolio.API.DTOs;

namespace MukeshPortfolio.API.Controllers
{
    /// <summary>
    /// Experiences API — portfolio ki professional work history expose karta hai.
    /// Filhaal in-memory list (Step 4 mein EF Core aayega).
    /// Route: /api/experiences
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ExperiencesController : ControllerBase
    {
        // ---------------------------------------------------------------
        // TEMPORARY: In-memory store for demo
        // ---------------------------------------------------------------
        private static readonly List<ExperienceDto> _experiences = new()
        {
            new ExperienceDto
            {
                Id = 1,
                CompanyName = "Previous Company",
                JobTitle = "Senior .NET Developer",
                Location = "Remote",
                StartDate = new DateTime(2019, 1, 15),
                EndDate = new DateTime(2024, 12, 31),
                IsCurrent = false,
                Description = "Designed and developed scalable REST APIs using ASP.NET Core. Led a team of 3 developers.",
                Technologies = "C#, ASP.NET Core, SQL Server, Entity Framework Core, Azure",
                CreatedAt = DateTime.UtcNow
            },
            new ExperienceDto
            {
                Id = 2,
                CompanyName = "Current Company",
                JobTitle = "Full Stack .NET Developer",
                Location = "Ahmedabad",
                StartDate = new DateTime(2025, 1, 1),
                EndDate = null,
                IsCurrent = true,
                Description = "Building full stack web applications using React and ASP.NET Core.",
                Technologies = "React, TypeScript, ASP.NET Core, SQL Server",
                CreatedAt = DateTime.UtcNow
            }
        };

        private static int _nextId = 3;

        // ================================================================
        // GET: /api/experiences
        // Saari experiences, latest first (StartDate descending).
        // ================================================================
        [HttpGet]
        public ActionResult<IEnumerable<ExperienceDto>> GetAll()
        {
            var sorted = _experiences
                .OrderByDescending(e => e.StartDate)
                .ToList();
            return Ok(sorted);
        }

        // ================================================================
        // GET: /api/experiences/{id}
        // ================================================================
        [HttpGet("{id:int}")]
        public ActionResult<ExperienceDto> GetById(int id)
        {
            var experience = _experiences.FirstOrDefault(e => e.Id == id);
            if (experience == null)
                return NotFound(new { message = $"Experience with id {id} not found" });

            return Ok(experience);
        }

        // ================================================================
        // GET: /api/experiences/current
        // Sirf currently working job(s).
        // Custom endpoint — practical use ke liye.
        // ================================================================
        [HttpGet("current")]
        public ActionResult<IEnumerable<ExperienceDto>> GetCurrent()
        {
            var current = _experiences.Where(e => e.IsCurrent).ToList();
            return Ok(current);
        }

        // ================================================================
        // POST: /api/experiences
        // Nayi experience create karta hai.
        // Agar EndDate null hai, to IsCurrent automatic true.
        // ================================================================
        [HttpPost]
        public ActionResult<ExperienceDto> Create([FromBody] ExperienceCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newExperience = new ExperienceDto
            {
                Id = _nextId++,
                CompanyName = dto.CompanyName,
                JobTitle = dto.JobTitle,
                Location = dto.Location,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsCurrent = dto.EndDate == null,
                Description = dto.Description,
                Technologies = dto.Technologies,
                CreatedAt = DateTime.UtcNow
            };

            _experiences.Add(newExperience);

            return CreatedAtAction(nameof(GetById), new { id = newExperience.Id }, newExperience);
        }

        // ================================================================
        // PUT: /api/experiences/{id}
        // Existing experience update karta hai.
        // ================================================================
        [HttpPut("{id:int}")]
        public ActionResult<ExperienceDto> Update(int id, [FromBody] ExperienceUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var experience = _experiences.FirstOrDefault(e => e.Id == id);
            if (experience == null)
                return NotFound(new { message = $"Experience with id {id} not found" });

            experience.CompanyName = dto.CompanyName;
            experience.JobTitle = dto.JobTitle;
            experience.Location = dto.Location;
            experience.StartDate = dto.StartDate;
            experience.EndDate = dto.EndDate;
            experience.IsCurrent = dto.EndDate == null;
            experience.Description = dto.Description;
            experience.Technologies = dto.Technologies;

            return Ok(experience);
        }

        // ================================================================
        // DELETE: /api/experiences/{id}
        // ================================================================
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var experience = _experiences.FirstOrDefault(e => e.Id == id);
            if (experience == null)
                return NotFound(new { message = $"Experience with id {id} not found" });

            _experiences.Remove(experience);
            return NoContent();
        }
    }
}