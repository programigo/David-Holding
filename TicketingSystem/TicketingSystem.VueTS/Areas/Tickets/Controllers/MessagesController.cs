using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Data.Constants;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Areas.Tickets.Models.Messages;
using TicketingSystem.VueTS.Areas.Tickets.Models.Tickets;
using TicketingSystem.VueTS.Infrastructure.Extensions;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;
using WEB_ENUMS = TicketingSystem.VueTS.Common.Enums;

namespace TicketingSystem.VueTS.Areas.Tickets.Controllers
{
    [Authorize]
    [Route("api/messages")]

    public class MessagesController : ControllerBase
    {
        private readonly IMessageService messages;
        private readonly ITicketService tickets;
        private readonly IUserService userManager;

        public MessagesController(IMessageService messages, ITicketService tickets, IUserService userManager)
        {
            this.messages = messages;
            this.tickets = tickets;
            this.userManager = userManager;
        }

        public IActionResult Create()
        {
            return Ok(new AddMessageFormModel
            {
                Tickets = GetTickets()
            });
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]AddMessageFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string authorId = this.userManager.GetUserId(User);

            MessageState? messageState = null;

            if (model.State != null)
            {
                messageState = (MessageState)Enum.Parse(typeof(MessageState), model.State.ToString());
            }

            this.messages.Create(model.Content, DateTime.UtcNow, messageState, model.TicketId, authorId);
        
            return StatusCode(201);
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

            return Ok(message);
        }
        
        [HttpPost("attachfiles/{id}")]
        public IActionResult AttachFiles(int id, IFormCollection files)
        {
            foreach (var file in files.Files)
            {
                if (!file.FileName.EndsWith(".zip")
                || file.Length > DataConstants.AttachedFileLength)
                {
        
                    return BadRequest();
                }
        
                byte[] fileContents = file.ToByteArray();
        
                bool success = this.messages.SaveFiles(id, fileContents);
        
                if (!success)
                {
                    return BadRequest();
                }
            }
        
            return Ok();
        }

        [HttpGet("downloadattached/{id}")]
        public IActionResult DownloadAttached(int id)
        {
            byte[] messageFiles = this.messages.GetAttachedFiles(id);
        
            if (messageFiles == null)
            {
                return BadRequest();
            }
        
            return File(messageFiles, "application/zip");
        }

        [HttpGet("tickets")]
        public IEnumerable<SelectListItem> GetTickets()
        {
            List<TicketViewModel> tickets = this.tickets.DropdownAll()
                .Select(t => new TicketViewModel
                {
                    Id = t.Id,
                    PostTime = t.PostTime,
                    Project = t.Project,
                    Sender = t.Sender,
                    SenderId = t.SenderId,
                    TicketType = (WEB_ENUMS.TicketType)Enum.Parse(typeof(WEB_ENUMS.TicketType), t.TicketType.ToString()),
                    TicketState = (WEB_ENUMS.TicketState)Enum.Parse(typeof(WEB_ENUMS.TicketState), t.TicketState.ToString()),
                    Title = t.Title,
                    Description = t.Description,
                    AttachedFiles = t.AttachedFiles
                })
                .ToList();

            var id = User.GetUserId();

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

            var result = ticketListItems.ToArray();

            return result;
        }
    }
}
