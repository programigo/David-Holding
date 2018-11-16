using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Areas.Projects.Models.Projects;
using TicketingSystem.VueTS.Common.Constants;
using TicketingSystem.VueTS.Models;

namespace TicketingSystem.VueTS.Areas.Projects.Controllers
{
    [Authorize]
    [Route("api/projects")]
    
    public class ProjectsController : ControllerBase
    {
        private readonly IAdminProjectService projects;

        public ProjectsController(IAdminProjectService projects)
        {
            this.projects = projects ?? throw new ArgumentNullException(nameof(projects));
        }

        [HttpGet("{page}")]
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

        [Authorize(Roles = WebConstants.AdministratorRole)]
        [HttpPost("create")]
        public IActionResult Create([FromBody]AddProjectFormModel model)
        {
            if (!ModelState.IsValid)
            {
                var err = ModelState.ToBadRequestErrorModel();
                return BadRequest(ModelState.ToBadRequestErrorModel());
            }

            this.projects.Create(model.Name, model.Description);

            return StatusCode(201);
        }

        [HttpGet("details/{id}")]
        public IActionResult Details([FromRoute(Name = "id")] int id)
        {
            ProjectModel project = this.projects.Details(id)
                .ProjectTo<ProjectModel>()
                .FirstOrDefault();

            return Ok(project);
        }

        [Authorize(Roles = WebConstants.AdministratorRole)]
        [HttpGet("edit/{id}")]
        public IActionResult Edit([FromRoute(Name = "id")] int id)
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

        [Authorize(Roles = WebConstants.AdministratorRole)]
        [HttpPut("edit/{id}")]
        public IActionResult Edit([FromRoute(Name = "id")] int id, [FromBody]AddProjectFormModel model)
        {
            bool updatedProject = this.projects.Edit(id, model.Name, model.Description);

            if (!updatedProject)
            {
                return NotFound();
            }

            return Ok();
        }

        [Authorize(Roles = WebConstants.AdministratorRole)]
        [HttpDelete("delete/{id}")]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            this.projects.Delete(id);

            return StatusCode(204);
        }
    }
}
