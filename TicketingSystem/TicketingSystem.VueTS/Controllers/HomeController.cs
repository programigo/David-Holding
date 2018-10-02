using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Areas.Projects.Models.Projects;

namespace TicketingSystem.VueTS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAdminProjectService projects;

        public HomeController(IAdminProjectService projects)
        {
            this.projects = projects;
        }

        [HttpGet("[action]")]
        public ProjectListingViewModel Index(int page = 1)
        {
            List<ProjectViewModel> projects = this.projects.All(page)
               .ProjectTo<ProjectViewModel>().ToList();

            return new ProjectListingViewModel
            {
                Projects = projects,
                TotalProjects = this.projects.Total(),
                CurrentPage = page
            };
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
