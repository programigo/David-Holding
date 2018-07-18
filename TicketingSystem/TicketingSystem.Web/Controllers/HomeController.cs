namespace TicketingSystem.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using TicketingSystem.Services.Admin;
    using Web.Areas.Projects.Models.Projects;
    using Web.Models;

    public class HomeController : Controller
    {
        private readonly IAdminProjectService projects;

        public HomeController(IAdminProjectService projects)
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
