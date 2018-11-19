using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using TicketingSystem.Services;
using DATA = TicketingSystem.Data;
using DATA_MODELS = TicketingSystem.Data.Models;
using DATA_ENUMS = TicketingSystem.Data.Enums;

namespace TicketingSystem.Implementations
{
	public class TicketService : ITicketService
	{
		private readonly DATA.TicketingSystemDbContext db;

		public TicketService(DATA.TicketingSystemDbContext db)
		{
			this.db = db;
		}

		public IQueryable<TicketListingServiceModel> All(int page = 1)
		=> this.db
			.Tickets
			.Skip((page - 1) * 10)
			.Take(10)
			.Select(t => new TicketListingServiceModel
			{
				Id = t.Id,
				PostTime = t.PostTime,
				ProjectId = t.ProjectId,
				Project = t.Project.Name,
				SenderId = t.SenderId,
				Sender = t.Sender.UserName,
				TicketType = (TicketType)Enum.Parse(typeof(TicketType), t.TicketType.ToString()),
				TicketState = (TicketState)Enum.Parse(typeof(TicketState), t.TicketState.ToString()),
				Title = t.Title,
				Description = t.Description,
				AttachedFiles = t.AttachedFiles
			})
			.AsQueryable();

		public IQueryable<TicketListingServiceModel> GetAllTickets()
			=> this.db
			.Tickets
			.Select(t => new TicketListingServiceModel
			{
				Id = t.Id,
				PostTime = t.PostTime,
				ProjectId = t.ProjectId,
				Project = t.Project.Name,
				SenderId = t.SenderId,
				Sender = t.Sender.UserName,
				TicketType = (TicketType)Enum.Parse(typeof(TicketType), t.TicketType.ToString()),
				TicketState = (TicketState)Enum.Parse(typeof(TicketState), t.TicketState.ToString()),
				Title = t.Title,
				Description = t.Description,
				AttachedFiles = t.AttachedFiles
			})
			.AsQueryable();

		public void Create(string title, string description, DateTime postTime, TicketType ticketType, TicketState? ticketState, string senderId, int projectId)
		{
			DATA_MODELS.Ticket ticket = new DATA_MODELS.Ticket
			{
				Title = title,
				Description = description,
				PostTime = postTime,
				TicketType = (DATA_ENUMS.TicketType)Enum.Parse(typeof(DATA_ENUMS.TicketType), ticketType.ToString()),
				TicketState = ticketState != null ? (DATA_ENUMS.TicketState)Enum.Parse(typeof(DATA_ENUMS.TicketState), ticketState.ToString()) : default(DATA_ENUMS.TicketState),
				SenderId = senderId,
				ProjectId = projectId
			};

			this.db.Add(ticket);

			this.db.SaveChanges();
		}

		public void Delete(int id)
		{
			DATA_MODELS.Ticket ticket = this.db.Tickets.Find(id);

			if (ticket == null)
			{
				return;
			}

			this.db.Remove(ticket);

			this.db.SaveChanges();
		}

		public IQueryable<TicketListingServiceModel> Details(int id)
		=> this.db
			.Tickets
			.Where(t => t.Id == id)
			.Select(t => new TicketListingServiceModel
			{
				Id = t.Id,
				PostTime = t.PostTime,
				ProjectId = t.ProjectId,
				Project = t.Project.Name,
				SenderId = t.SenderId,
				Sender = t.Sender.UserName,
				TicketType = (TicketType)Enum.Parse(typeof(TicketType), t.TicketType.ToString()),
				TicketState = (TicketState)Enum.Parse(typeof(TicketState), t.TicketState.ToString()),
				Title = t.Title,
				Description = t.Description,
				AttachedFiles = t.AttachedFiles,
				Messages = t.Messages.Select(m => new MessageListingServiceModel
				{
					Id = m.Id,
					PostDate = m.PostDate,
					AuthorId = m.AuthorId,
					Author = m.Author.UserName,
					TicketId = m.TicketId,
					State = (MessageState)Enum.Parse(typeof(MessageState), m.State.ToString()),
					Content = m.Content,
					AttachedFiles = m.AttachedFiles
				}).ToList()
			})
			.AsQueryable();

		public IQueryable<TicketListingServiceModel> DropdownAll()
		=> this.db
			.Tickets
			.Select(t => new TicketListingServiceModel
			{
				Id = t.Id,
				PostTime = t.PostTime,
				ProjectId = t.ProjectId,
				Project = t.Project.Name,
				SenderId = t.SenderId,
				Sender = t.Sender.UserName,
				TicketType = (TicketType)Enum.Parse(typeof(TicketType), t.TicketType.ToString()),
				TicketState = (TicketState)Enum.Parse(typeof(TicketState), t.TicketState.ToString()),
				Title = t.Title,
				Description = t.Description,
				AttachedFiles = t.AttachedFiles
			})
			.AsQueryable();

		public bool Edit(int id, string title, string description, TicketType ticketType, TicketState ticketState)
		{
			DATA_MODELS.Ticket ticket = this.db.Tickets.FirstOrDefault(t => t.Id == id);

			if (ticket == null)
			{
				return false;
			}

			ticket.Title = title;
			ticket.Description = description;
			ticket.TicketType = (DATA_ENUMS.TicketType)Enum.Parse(typeof(DATA_ENUMS.TicketType), ticketType.ToString());
			ticket.TicketState = (DATA_ENUMS.TicketState)Enum.Parse(typeof(DATA_ENUMS.TicketState), ticketState.ToString());

			this.db.SaveChanges();

			return true;
		}

		public byte[] GetAttachedFiles(int ticketId)
		{
			DATA_MODELS.Ticket ticket = this.db.Find<DATA_MODELS.Ticket>(ticketId);

			if (ticket == null)
			{
				return null;
			}

			return ticket.AttachedFiles;
		}

		public bool SaveFiles(int ticketId, byte[] attachedFiles)
		{
			DATA_MODELS.Ticket ticket = this.db.Find<DATA_MODELS.Ticket>(ticketId);

			if (ticket == null)
			{
				return false;
			}

			ticket.AttachedFiles = attachedFiles;

			this.db.SaveChanges();

			return true;
		}

		public int Total() => this.db.Tickets.Count();

		public Project GetProject(int id)
		=> this.db
				.Projects
				.Where(p => p.Id == id)
				.Select(p => new Project
				{
					Name = p.Name,
					Description = p.Description
				})
				.FirstOrDefault();

		private Ticket GetMessageTicket(int id)
		=> this.db
				.Messages
				.Where(m => m.Id == id)
				.Select(m => m.Ticket)
				.Select(t => new Ticket
				{
					Title = t.Title,
					Description = t.Description,
					AttachedFiles = t.AttachedFiles
				})
				.FirstOrDefault();
	}
}
