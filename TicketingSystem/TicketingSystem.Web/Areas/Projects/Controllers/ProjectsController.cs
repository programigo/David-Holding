using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Services.Admin;
using TicketingSystem.Web.Areas.Projects.Models.Projects;
using TicketingSystem.Web.Infrastructure.Extensions;

namespace TicketingSystem.Web.Areas.Projects.Controllers
{
    [Area("Projects")]
    public class ProjectsController : Controller
    {
        private readonly IAdminProjectService projects;

        public ProjectsController(IAdminProjectService projects)
        {
            this.projects = projects;
        }

        public IActionResult Index(int page = 1)
        {
            List<ProjectViewModel> projects = this.projects.All(page)
                .ProjectTo<ProjectViewModel>().ToList();

            return View(new ProjectListingViewModel
            {
                Projects = projects,
                TotalProjects = this.projects.Total(),
                CurrentPage = page
            });
        }
        
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(AddProjectFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.projects.Create(model.Name, model.Description);

            TempData.AddSuccessMessage($"Project {model.Name} created successfully");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            ProjectViewModel project = this.projects.Details(id)
                .ProjectTo<ProjectViewModel>()
                .FirstOrDefault();

            return View(project);
        }

        public IActionResult Edit(int id)
        {
            ProjectViewModel project = this.projects.Details(id)
                .ProjectTo<ProjectViewModel>()
                .FirstOrDefault();
        
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(int id, AddProjectFormModel model)
        {
            bool updatedProject = this.projects.Edit(id, model.Name, model.Description);

            if (!updatedProject)
            {
                return NotFound();
            }

            TempData.AddSuccessMessage($"Project {model.Name} edited successfully");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            this.projects.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
