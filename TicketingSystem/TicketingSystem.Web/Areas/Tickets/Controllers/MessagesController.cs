using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Common.Constants;
using TicketingSystem.Common.Enums;
using TicketingSystem.Data.Models;
using TicketingSystem.Services.Tickets;
using TicketingSystem.Web.Areas.Tickets.Models.Messages;
using TicketingSystem.Web.Areas.Tickets.Models.Tickets;
using TicketingSystem.Web.Infrastructure.Extensions;

namespace TicketingSystem.Web.Areas.Tickets.Controllers
{
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

            string authorId = this.userManager.GetUserId(User);

            this.messages.Create(model.Content, DateTime.UtcNow, model.State, model.TicketId, authorId);
        
            TempData.AddSuccessMessage("Message created successfully.");
        
            return RedirectToAction(nameof(TicketsController.Index), "Tickets");
        }

        public IActionResult AttachFiles(int id)
        {
            MessageViewModel message = this.messages.Details(id)
                .ProjectTo<MessageViewModel>().FirstOrDefault();

            return View(message);
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
        
                byte[] fileContents = file.ToByteArray();
        
                bool success = this.messages.SaveFiles(id, fileContents);
        
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
            byte[] messageFiles = this.messages.GetAttachedFiles(id);
        
            if (messageFiles == null)
            {
                return BadRequest();
            }
        
            return File(messageFiles, "application/zip");
        }

        private IEnumerable<SelectListItem> GetTickets()
        {
            List<TicketViewModel> tickets = this.tickets.DropdownAll()
                .ProjectTo<TicketViewModel>().ToList();

            List<SelectListItem> ticketListItems = new List<SelectListItem>();

            bool isAuthorized = User.IsInRole("Administrator") || User.IsInRole("Support");

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
