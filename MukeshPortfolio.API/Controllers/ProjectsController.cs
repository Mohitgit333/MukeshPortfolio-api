using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MukeshPortfolio.API.DTOs;

namespace MukeshPortfolio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private static readonly List<ProjectDto> _projects = new()
        {
            new ProjectDto
            {
                Id = 1,
                Title = "Portfolio Web Portal",
                Description = "Full Stack portfolio built with React + TypeScript and ASP.NET Core Web API.",
                Technologies = "React, TypeScript, ASP.NET Core, SQL Server",
                GitHubUrl = "https://github.com/Mohitgit333",
                CreatedAt = DateTime.UtcNow
            },
            new ProjectDto
            {
                Id = 2,
                Title = "E-Commerce REST API",
                Description = "A scalable e-commerce backend with authentication, cart, and order management.",
                Technologies = "ASP.NET Core, EF Core, JWT, SQL Server",
                GitHubUrl = "https://github.com/Mohitgit333",
                CreatedAt = DateTime.UtcNow
            }
        };

        private static int _nextId = 3;

        // ================================================================
        // GET: /api/projects
        // ================================================================
        [HttpGet]
        public ActionResult<IEnumerable<ProjectDto>> GetAll()
        {
            return Ok(_projects);
        }

        // ================================================================
        // GET: /api/projects/{id}
        // ================================================================
        [HttpGet("{id:int}")]
        public ActionResult<ProjectDto> GetById(int id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound(new { message = $"Project with id {id} not found" });

            return Ok(project);
        }

        // ================================================================
        // POST: /api/projects
        // ================================================================
        [HttpPost]
        public ActionResult<ProjectDto> Create([FromBody] ProjectCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newProject = new ProjectDto
            {
                Id = _nextId++,
                Title = dto.Title,
                Description = dto.Description,
                Technologies = dto.Technologies,
                GitHubUrl = dto.GitHubUrl,
                LiveDemoUrl = dto.LiveDemoUrl,
                ImageUrl = dto.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            _projects.Add(newProject);

            return CreatedAtAction(nameof(GetById), new { id = newProject.Id }, newProject);
        }

        // ================================================================
        // PUT: /api/projects/{id}
        // ================================================================
        [HttpPut("{id:int}")]
        public ActionResult<ProjectDto> Update(int id, [FromBody] ProjectUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound(new { message = $"Project with id {id} not found" });

            project.Title = dto.Title;
            project.Description = dto.Description;
            project.Technologies = dto.Technologies;
            project.GitHubUrl = dto.GitHubUrl;
            project.LiveDemoUrl = dto.LiveDemoUrl;
            project.ImageUrl = dto.ImageUrl;

            return Ok(project);
        }

        // ================================================================
        // DELETE: /api/projects/{id}
        // ================================================================
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound(new { message = $"Project with id {id} not found" });

            _projects.Remove(project);
            return NoContent();
        }
    }
}
