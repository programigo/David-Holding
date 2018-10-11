using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Areas.Projects.Models.Projects;
using TicketingSystem.VueTS.Infrastructure.Extensions;

namespace TicketingSystem.VueTS.Controllers
{
    [Authorize]
    [Route("api/projects")]
    
    public class ProjectsController : Controller
    {
        private readonly IAdminProjectService projects;

        public ProjectsController(IAdminProjectService projects)
        {
            this.projects = projects;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(int page = 1)
        {
            var projects = this.projects.All(page)
                .ProjectTo<ProjectModel>()
                .ToArray();

            return Ok(new ProjectListingModel
            {
                Projects = projects,
                TotalProjects = this.projects.Total(),
                CurrentPage = page
            });
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]AddProjectFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            this.projects.Create(model.Name, model.Description);

            TempData.AddSuccessMessage($"Project {model.Name} created successfully");

            return StatusCode(201);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            ProjectModel project = this.projects.Details(id)
                .ProjectTo<ProjectModel>()
                .FirstOrDefault();

            return Ok(project);
        }

        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            ProjectModel project = this.projects.Details(id)
                .ProjectTo<ProjectModel>()
                .FirstOrDefault();
        
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, [FromBody]AddProjectFormModel model)
        {
            bool updatedProject = this.projects.Edit(id, model.Name, model.Description);

            if (!updatedProject)
            {
                return NotFound();
            }

            TempData.AddSuccessMessage($"Project {model.Name} edited successfully");

            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            this.projects.Delete(id);

            return StatusCode(204);
        }
    }
}
