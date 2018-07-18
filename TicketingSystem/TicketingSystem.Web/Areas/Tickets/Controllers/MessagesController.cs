namespace TicketingSystem.Web.Areas.Tickets.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TicketingSystem.Data;
    using TicketingSystem.Data.Models;
    using TicketingSystem.Services.Tickets;
    using TicketingSystem.Web.Areas.Tickets.Models.Messages;
    using TicketingSystem.Web.Infrastructure.Extensions;

    [Area("Tickets")]
    public class MessagesController : Controller
    {
        private readonly IMessageService messages;
        private readonly ITicketService tickets;
        private readonly UserManager<User> userManager;

        public MessagesController(IMessageService messages, ITicketService tickets, UserManager<User> userManager)
        {
            this.messages = messages;
            this.tickets = tickets;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return View(new AddMessageFormModel
            {
                Tickets = GetTickets()
            });
        }

        [HttpPost]
        public IActionResult Create(AddMessageFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var authorId = this.userManager.GetUserId(User);

            this.messages.Create(model.Content, DateTime.UtcNow, model.State, model.TicketId, authorId);
        
            TempData.AddSuccessMessage("Message created successfully.");
        
            return RedirectToAction(nameof(TicketsController.Index), "Tickets");
        }

        public IActionResult AttachFiles(int id)
        {
            return View(this.messages.Details(id));
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
        
                var success = this.messages.SaveFiles(id, fileContents);
        
                if (!success)
                {
                    return BadRequest();
                }
            }
        
            TempData.AddSuccessMessage("File attached successfully");
        
            return RedirectToAction(nameof(TicketsController.Index), "Tickets");
        }
        
        public IActionResult DownloadAttached(int id)
        {
            var messageFiles = this.messages.GetAttachedFiles(id);
        
            if (messageFiles == null)
            {
                return BadRequest();
            }
        
            return File(messageFiles, "application/zip");
        }

        private IEnumerable<SelectListItem> GetTickets()
        {
            var tickets = this.tickets.DropdownAll();

            List<SelectListItem> ticketListItems = new List<SelectListItem>();

            var isAuthorized = User.IsInRole("Administrator") || User.IsInRole("Support");

            if (!isAuthorized)
            {
                ticketListItems = tickets
                .Where(t => t.SenderId == User.GetUserId() && t.TicketState != TicketState.Completed)
                .Select(t => new SelectListItem
                {
                    Text = t.Title,
                    Value = t.Id.ToString()
                })
                .ToList();
            }
            else
            {
                ticketListItems = tickets
                .Where(t => t.TicketState != TicketState.Completed)
                .Select(t => new SelectListItem
                {
                    Text = t.Title,
                    Value = t.Id.ToString()
                })
                .ToList();
            }

            return ticketListItems;
        }
    }
}
