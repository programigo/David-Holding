using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TicketingSystem.Services.Admin;
using TicketingSystem.Web.Areas.Projects.Models.Projects;
using TicketingSystem.Web.Models;

namespace TicketingSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminProjectService projects;

        public HomeController(IAdminProjectService projects)
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
