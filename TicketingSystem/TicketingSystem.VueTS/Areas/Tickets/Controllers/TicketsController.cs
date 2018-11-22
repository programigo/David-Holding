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
using TicketingSystem.VueTS.Common.Constants;
using TicketingSystem.VueTS.Infrastructure.Extensions;
using TicketingSystem.VueTS.Models;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;
using WEB_ENUMS = TicketingSystem.VueTS.Common.Enums;

namespace TicketingSystem.VueTS.Areas.Tickets.Controllers
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
			this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
			this.projects = projects ?? throw new ArgumentNullException(nameof(projects));
			this.tickets = tickets ?? throw new ArgumentNullException(nameof(tickets));
			this.messages = messages ?? throw new ArgumentNullException(nameof(messages));
		}

		[HttpGet("{page}")]
		public IActionResult Index(int page = 1)
		{
			var ticketListingModel = new TicketListingViewModel();

			if (User.IsInRole(WebConstants.AdministratorRole) || User.IsInRole(WebConstants.SuportRole))
			{
				TicketViewModel[] tickets = this.tickets.All(page)
			   .Select(ConvertTicket)
			   .ToArray();

				ticketListingModel.Tickets = tickets;
				ticketListingModel.TotalTickets = this.tickets.Total();
			}
			else
			{
				TicketViewModel[] tickets = this.tickets
				.GetAllTickets()
				.Where(t => t.Sender == User.Identity.Name)
				.Select(ConvertTicket)
				.ToArray();

				TicketViewModel[] result = All(tickets, page);

				ticketListingModel.Tickets = result;
				ticketListingModel.TotalTickets = tickets.Count();
			}

			return Ok(ticketListingModel);
		}

		[HttpPost("create")]
		public IActionResult Create([FromBody]SubmitTicketFormModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.ToBadRequestErrorModel());
			}

			model.Projects = GetProjects();

			string senderId = this.userManager.GetUserId(User);

			TicketType ticketType = (TicketType)Enum.Parse(typeof(TicketType), model.TicketType.ToString());

			TicketState? ticketState = null;

			if (model.TicketState != null)
			{
				ticketState = (TicketState)Enum.Parse(typeof(TicketState), model.TicketState.ToString());
			}

			this.tickets.Create(model.Title, model.Description, DateTime.UtcNow, ticketType, ticketState, senderId, model.ProjectId);

			return StatusCode(201);
		}

		[HttpPost("attachfiles/{id}")]
		public IActionResult AttachFiles([FromRoute(Name = "id")] int id, IFormCollection files)
		{
			var file = files.Files.Last();

			if (!file.FileName.EndsWith(".zip")
			|| file.Length > DataConstants.AttachedFileLength)
			{
				ModelState.AddModelError(string.Empty, "Your file should be .zip with maximum size of 2 MB.");
				return BadRequest(ModelState.ToBadRequestErrorModel());
			}

			byte[] fileContents = file.ToByteArray();

			bool success = this.tickets.SaveFiles(id, fileContents);

			if (!success)
			{
				return BadRequest();
			}

			return Ok();
		}

		[HttpGet("downloadattached/{id}")]
		public IActionResult DownloadAttached([FromRoute(Name = "id")] int id)
		{
			byte[] ticketFiles = this.tickets.GetAttachedFiles(id);

			if (ticketFiles == null)
			{
				return BadRequest();
			}

			return File(ticketFiles, "application/zip");
		}

		[Authorize(Roles = WebConstants.AdministratorRole + ", " + WebConstants.SuportRole)]
		[HttpPost("edit/{id}")]
		public IActionResult Edit([FromRoute(Name = "id")] int id, [FromBody]SubmitTicketFormModel model)
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
		public IActionResult Details([FromRoute(Name = "id")] int id)
		{
			TicketViewModel ticket = this.tickets.Details(id)
				.Select(CreateTicketViewModel)
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

		[Authorize(Roles = WebConstants.AdministratorRole + ", " + WebConstants.SuportRole)]
		[HttpDelete("delete/{id}")]
		public IActionResult Delete([FromRoute(Name = "id")] int id)
		{
			TicketViewModel ticket = this.tickets.Details(id)
				.Select(CreateTicketViewModel)
				.FirstOrDefault();

			if (ticket == null)
			{
				return NotFound("The ticket does not exist.");
			}

			this.tickets.Delete(id);

			return StatusCode(204);
		}

		[HttpGet("projects")]
		public IEnumerable<SelectListItem> GetProjects()
		{
			IEnumerable<ProjectListingServiceModel> projects = this.projects.DropdownAll();

			var projectListItems = projects
				.Select(p => new SelectListItem
				{
					Text = p.Name,
					Value = p.Id.ToString()
				})
				.ToArray();

			return projectListItems;
		}

		public static TicketViewModel CreateTicketViewModel(TicketListingServiceModel serviceTicket)
		{
			var model = new TicketViewModel
			{
				Id = serviceTicket.Id,
				PostTime = serviceTicket.PostTime,
				Project = serviceTicket.Project,
				Sender = serviceTicket.Sender,
				TicketType = (WEB_ENUMS.TicketType)Enum.Parse(typeof(WEB_ENUMS.TicketType), serviceTicket.TicketType.ToString()),
				TicketState = (WEB_ENUMS.TicketState)Enum.Parse(typeof(WEB_ENUMS.TicketState), serviceTicket.TicketState.ToString()),
				Title = serviceTicket.Title,
				Description = serviceTicket.Description,
				AttachedFiles = serviceTicket.AttachedFiles
			};

			return model;
		}

		public static TicketViewModel ConvertTicket(TicketListingServiceModel serviceTicket)
		{
			var ticket = new TicketViewModel
			{
				Id = serviceTicket.Id,
				PostTime = serviceTicket.PostTime,
				ProjectId = serviceTicket.ProjectId,
				Project = serviceTicket.Project,
				Sender = serviceTicket.Sender,
				TicketType = (WEB_ENUMS.TicketType)Enum.Parse(typeof(WEB_ENUMS.TicketType), serviceTicket.TicketType.ToString()),
				TicketState = (WEB_ENUMS.TicketState)Enum.Parse(typeof(WEB_ENUMS.TicketState), serviceTicket.TicketState.ToString()),
				Title = serviceTicket.Title,
				Description = serviceTicket.Description,
				AttachedFiles = serviceTicket.AttachedFiles
			};

			return ticket;
		}

		private TicketViewModel[] All(TicketViewModel[] collection, int page = 1)
			=> collection
				.Skip((page - 1) * 10)
				.Take(10)
				.ToArray();
	}
}
