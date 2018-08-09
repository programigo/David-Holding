using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Common.Constants;
using TicketingSystem.Services;
using TicketingSystem.Web.Areas.Tickets.Models.Messages;
using TicketingSystem.Web.Areas.Tickets.Models.Tickets;
using TicketingSystem.Web.Infrastructure.Extensions;
using WEB_ENUMS = TicketingSystem.Web.Common.Enums;
using DATA_MODELS = TicketingSystem.Data.Models;

namespace TicketingSystem.Web.Areas.Tickets.Controllers
{
    [Area("Tickets")]
    public class MessagesController : Controller
    {
        private readonly IMessageService messages;
        private readonly ITicketService tickets;
        private readonly UserManager<DATA_MODELS.User> userManager;

        public MessagesController(IMessageService messages, ITicketService tickets, UserManager<DATA_MODELS.User> userManager)
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

            MessageState messageState = (MessageState)Enum.Parse(typeof(MessageState), model.State.ToString());

            this.messages.Create(model.Content, DateTime.UtcNow, messageState, model.TicketId, authorId);
        
            TempData.AddSuccessMessage("Message created successfully.");
        
            return RedirectToAction(nameof(TicketsController.Index), "Tickets");
        }

        public IActionResult AttachFiles(int id)
        {
            MessageViewModel message = this.messages.Details(id)
                .Select(m => new MessageViewModel
                {
                    Id = m.Id,
                    PostDate = m.PostDate,
                    Author = m.Author,
                    Content = m.Content
                })
                .FirstOrDefault();

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
                .Select(t => new TicketViewModel
                {
                    Id = t.Id,
                    PostTime = t.PostTime,
                    Project = t.Project,
                    Sender = t.Sender,
                    TicketType = (WEB_ENUMS.TicketType)Enum.Parse(typeof(WEB_ENUMS.TicketType), t.TicketType.ToString()),
                    TicketState = (WEB_ENUMS.TicketState)Enum.Parse(typeof(WEB_ENUMS.TicketState), t.TicketState.ToString()),
                    Title = t.Title,
                    Description = t.Description,
                    AttachedFiles = t.AttachedFiles
                })
                .ToList();

            List<SelectListItem> ticketListItems = new List<SelectListItem>();

            bool isAuthorized = User.IsInRole("Administrator") || User.IsInRole("Support");

            if (!isAuthorized)
            {
                ticketListItems = tickets
                .Where(t => t.SenderId == User.GetUserId() && t.TicketState != WEB_ENUMS.TicketState.Completed)
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
                .Where(t => t.TicketState != WEB_ENUMS.TicketState.Completed)
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
