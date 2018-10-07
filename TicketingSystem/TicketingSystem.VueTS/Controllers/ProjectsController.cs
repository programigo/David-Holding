﻿using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Areas.Projects.Models.Projects;
using TicketingSystem.VueTS.Infrastructure.Extensions;

namespace TicketingSystem.VueTS.Controllers
{
    [Route("api/[controller]")]
    
    public class ProjectsController : Controller
    {
        private readonly IAdminProjectService projects;

        public ProjectsController(IAdminProjectService projects)
        {
            this.projects = projects;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var projects = this.projects.All(page)
                //.ProjectTo<ProjectViewModel>()
                .ToArray();

            return Ok(projects);
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

        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var project = this.projects.Details(id)
                //.ProjectTo<ProjectViewModel>()
                .FirstOrDefault();

            return Ok(project);
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
