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
using TicketingSystem.VueTS.Models;
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
            this.messages = messages ?? throw new ArgumentNullException(nameof(messages));
            this.tickets = tickets ?? throw new ArgumentNullException(nameof(tickets));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody]AddMessageFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.ToBadRequestErrorModel());
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
                .Select(ConvertMessage)
                .FirstOrDefault();

            return Ok(message);
        }

        [HttpPost("attachfiles/{id}")]
        public IActionResult AttachFiles([FromRoute(Name = "id")] int id, IFormCollection files)
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
        public IActionResult DownloadAttached([FromRoute(Name = "id")] int id)
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
                .Select(ConvertTicket)
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

        private static MessageViewModel ConvertMessage(MessageListingServiceModel serviceMessage)
        {
            var message = new MessageViewModel
            {
                Id = serviceMessage.Id,
                PostDate = serviceMessage.PostDate,
                Author = serviceMessage.Author,
                Content = serviceMessage.Content
            };

            return message;
        }

        private static TicketViewModel ConvertTicket(TicketListingServiceModel serviceTicket)
        {
            var ticket = new TicketViewModel
            {
                Id = serviceTicket.Id,
                PostTime = serviceTicket.PostTime,
                Project = serviceTicket.Project,
                Sender = serviceTicket.Sender,
                SenderId = serviceTicket.SenderId,
                TicketType = (WEB_ENUMS.TicketType)Enum.Parse(typeof(WEB_ENUMS.TicketType), serviceTicket.TicketType.ToString()),
                TicketState = (WEB_ENUMS.TicketState)Enum.Parse(typeof(WEB_ENUMS.TicketState), serviceTicket.TicketState.ToString()),
                Title = serviceTicket.Title,
                Description = serviceTicket.Description,
                AttachedFiles = serviceTicket.AttachedFiles
            };

            return ticket;
        }
    }
}
