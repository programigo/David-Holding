namespace TicketingSystem.Web.Areas.Tickets.Controllers
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Admin;
    using Services.Tickets;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Web.Areas.Tickets.Models.Tickets;
    using Web.Infrastructure.Extensions;

    [Area(WebConstants.TicketsArea)]
    [Authorize(Roles = WebConstants.AdministratorRole + ", " + WebConstants.SuportRole + ", " + WebConstants.ClientRole)]
    public class TicketsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IAdminProjectService projects;
        private readonly ITicketService tickets;
        private readonly IMessageService messages;

        public TicketsController(UserManager<User> userManager, IAdminProjectService projects, ITicketService tickets, IMessageService messages)
        {
            this.userManager = userManager;
            this.projects = projects;
            this.tickets = tickets;
            this.messages = messages;
        }

        public IActionResult Index(int page = 1)
        => View(new TicketListingViewModel
        {
            Tickets = this.tickets.All(page),
            TotalTickets = this.tickets.Total(),
            CurrentPage = page
        });

        public IActionResult Create()
        {
            return View(new SubmitTicketFormModel
            {
                Projects = GetProjects()
            });
        }

        [HttpPost]
        public IActionResult Create(SubmitTicketFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Projects = GetProjects();
                return View(model);
            }

            var senderId = this.userManager.GetUserId(User);

            this.tickets.Create(model.Title, model.Description, DateTime.UtcNow, model.TicketType, model.TicketState, senderId, model.ProjectId);

            TempData.AddSuccessMessage($"Ticket {model.Title} successfully sended.");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AttachFiles(int id)
        {
            return View(this.tickets.Details(id));
        }

        [HttpPost]
        public IActionResult AttachFiles(int id, IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (!file.FileName.EndsWith(".zip")
                || file.Length > DataConstants.AttachedFileLength)
                {
                    TempData.AddErrorMessage("Your submission file should be a '.zip' file with no more than 2MB in size!");

                    return RedirectToAction(nameof(AttachFiles), new { id });
                }

                var fileContents = file.ToByteArray();
        
                var success = this.tickets.SaveFiles(id, fileContents);
        
                if (!success)
                {
                    return BadRequest();
                }
            }

            TempData.AddSuccessMessage("File attached successfully");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DownloadAttached(int id)
        {
            var ticketFiles = this.tickets.GetAttachedFiles(id);

            if (ticketFiles == null)
            {
                return BadRequest();
            }

            return File(ticketFiles, "application/zip");
        }

        public IActionResult Edit(int id)
        {
            var ticket = this.tickets.Details(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost]
        public IActionResult Edit(int id, SubmitTicketFormModel model)
        {
            var updatedTicket = this.tickets.Edit(id, model.TicketType, model.TicketState);

            if (!updatedTicket)
            {
                return NotFound();
            }

            TempData.AddSuccessMessage($"Ticket {model.Title} edited successfully");

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var ticket = this.tickets.Details(id);
            var messages = this.messages.All().Where(m => m.TicketId == id).ToList();
            ticket.Messages = messages;

            return View(ticket);
        }

        public IActionResult Delete(int id)
        {
            this.tickets.Delete(id);

            TempData.AddSuccessMessage($"Ticket deleted successfully");

            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<SelectListItem> GetProjects()
        {
            var projects = this.projects.DropdownAll();

            var projectListItems = projects
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                })
                .ToList();

            return projectListItems;
        }
    }
}
