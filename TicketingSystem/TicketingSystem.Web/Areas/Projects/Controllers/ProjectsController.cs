namespace TicketingSystem.Web.Areas.Projects.Controllers
{
    using Areas.Projects.Models.Projects;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Mvc;
    using Services.Admin;

    [Area("Projects")]
    public class ProjectsController : Controller
    {
        private readonly IAdminProjectService projects;

        public ProjectsController(IAdminProjectService projects)
        {
            this.projects = projects;
        }

        public IActionResult Index(int page = 1)
        => View(new ProjectListingViewModel
        {
            Projects = this.projects.All(page),
            TotalProjects = this.projects.Total(),
            CurrentPage = page
        });

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
            return View(this.projects.Details(id));
        }

        public IActionResult Edit(int id)
        {
            var project = this.projects.Details(id);
        
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(int id, AddProjectFormModel model)
        {
            var updatedProject = this.projects.Edit(id, model.Name, model.Description);

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
