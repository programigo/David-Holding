using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Data.Constants;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Areas.Tickets.Models.Messages;
using TicketingSystem.VueTS.Areas.Tickets.Models.Tickets;
using TicketingSystem.VueTS.Common.Constants;
using TicketingSystem.VueTS.Infrastructure.Extensions;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;
using WEB_ENUMS = TicketingSystem.VueTS.Common.Enums;

namespace TicketingSystem.Web.Areas.Tickets.Controllers
{
    [Authorize]
    [Route("api/tickets")]

    public class TicketsController : ControllerBase
    {
        private readonly IUserService userManager;
        private readonly IAdminProjectService projects;
        private readonly ITicketService tickets;
        private readonly IMessageService messages;

        public TicketsController(IUserService userManager, IAdminProjectService projects, ITicketService tickets, IMessageService messages)
        {
            this.userManager = userManager;
            this.projects = projects;
            this.tickets = tickets;
            this.messages = messages;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            var tickets = this.tickets.All(page)
                .Select(t => new TicketViewModel
                {
                    Id = t.Id,
                    PostTime = t.PostTime,
                    ProjectId = t.ProjectId,
                    Project = t.Project,
                    Sender = t.Sender,
                    TicketType = (WEB_ENUMS.TicketType)Enum.Parse(typeof(WEB_ENUMS.TicketType), t.TicketType.ToString()),
                    TicketState = (WEB_ENUMS.TicketState)Enum.Parse(typeof(WEB_ENUMS.TicketState), t.TicketState.ToString()),
                    Title = t.Title,
                    Description = t.Description,
                    AttachedFiles = t.AttachedFiles
                })
                .ToArray();

            return Ok(new TicketListingViewModel
               {
                   Tickets = tickets,
                   TotalTickets = this.tickets.Total(),
                   CurrentPage = page
               });
        }
        

        public IActionResult Create()
        => Ok(new SubmitTicketFormModel
            {
                Projects = GetProjects()
            });

        [HttpPost("create")]
        public IActionResult Create(SubmitTicketFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Projects = GetProjects();
                return Ok(model);
            }

            string senderId = this.userManager.GetUserId(User);

            TicketType ticketType = (TicketType)Enum.Parse(typeof(TicketType), model.TicketType.ToString());

            TicketState ticketState = (TicketState)Enum.Parse(typeof(TicketState), model.TicketState.ToString());

            this.tickets.Create(model.Title, model.Description, DateTime.UtcNow, ticketType, ticketState, senderId, model.ProjectId);

            return StatusCode(201);
        }

        public IActionResult AttachFiles(int id)
        {
            TicketViewModel ticket = this.tickets.Details(id)
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
                .FirstOrDefault();

            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult AttachFiles(int id, IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (!file.FileName.EndsWith(".zip")
                || file.Length > DataConstants.AttachedFileLength)
                {
                    return RedirectToAction(nameof(AttachFiles), new { id });
                }

                byte[] fileContents = file.ToByteArray();
        
                bool success = this.tickets.SaveFiles(id, fileContents);
        
                if (!success)
                {
                    return BadRequest();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult DownloadAttached(int id)
        {
            byte[] ticketFiles = this.tickets.GetAttachedFiles(id);

            if (ticketFiles == null)
            {
                return BadRequest();
            }

            return File(ticketFiles, "application/zip");
        }

        public IActionResult Edit(int id)
        {
            TicketViewModel ticket = this.tickets.Details(id)
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
                .FirstOrDefault();

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id, SubmitTicketFormModel model)
        {
            TicketType ticketType = (TicketType)Enum.Parse(typeof(TicketType), model.TicketType.ToString());

            TicketState ticketState = (TicketState)Enum.Parse(typeof(TicketState), model.TicketState.ToString());

            bool updatedTicket = this.tickets.Edit(id, model.Title, model.Description, ticketType, ticketState);

            if (!updatedTicket)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            TicketViewModel ticket = this.tickets.Details(id)
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
                .FirstOrDefault();

            List<MessageViewModel> messages = this.messages.All()
                .Where(m => m.TicketId == id)
                .Select(m => new MessageViewModel
                {
                    Id = m.Id,
                    PostDate = m.PostDate,
                    Author = m.Author,
                    Content = m.Content,
                    AttachedFiles = m.AttachedFiles
                })
                .ToList();

            ticket.Messages = messages;

            return Ok(ticket);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            TicketViewModel ticket = this.tickets.Details(id)
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
                .FirstOrDefault();

            this.tickets.Delete(id);

            return StatusCode(204);
        }

        private IEnumerable<SelectListItem> GetProjects()
        {
            IEnumerable<ProjectListingServiceModel> projects = this.projects.DropdownAll();

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
