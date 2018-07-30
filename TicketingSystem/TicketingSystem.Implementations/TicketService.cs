using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Common.Enums;
using TicketingSystem.Data;
using TicketingSystem.Data.Models;
using TicketingSystem.Services.Tickets;
using TicketingSystem.Services.Tickets.Models;

namespace TicketingSystem.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly TicketingSystemDbContext db;
        private readonly UserManager<User> userManager;

        public TicketService(TicketingSystemDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
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
                Project = t.Project,
                SenderId = t.SenderId,
                Sender = t.Sender.UserName,
                TicketType = t.TicketType,
                TicketState = t.TicketState,
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
                    Ticket = m.Ticket,
                    State = m.State,
                    Content = m.Content,
                    AttachedFiles = m.AttachedFiles
                }).ToList()
            })
            .AsQueryable();
        
        public void Create(string title, string description, DateTime postTime, TicketType ticketType, TicketState ticketState, string senderId, int projectId)
        {
            Ticket ticket = new Ticket
            {
                Title = title,
                Description = description,
                PostTime = postTime,
                TicketType = ticketType,
                TicketState = ticketState,
                SenderId = senderId,
                ProjectId = projectId
            };

            this.db.Add(ticket);

            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            Ticket ticket = this.db.Tickets.Find(id);

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
                Project = t.Project,
                SenderId = t.SenderId,
                Sender = t.Sender.UserName,
                TicketType = t.TicketType,
                TicketState = t.TicketState,
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
                    Ticket = m.Ticket,
                    State = m.State,
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
                Project = t.Project,
                SenderId = t.SenderId,
                Sender = t.Sender.UserName,
                TicketType = t.TicketType,
                TicketState = t.TicketState,
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
                    Ticket = m.Ticket,
                    State = m.State,
                    Content = m.Content,
                    AttachedFiles = m.AttachedFiles
                }).ToList()
            })
            .AsQueryable();

        public bool Edit(int id, string title, string description, TicketType ticketType, TicketState ticketState)
        {
            Ticket ticket = this.db.Tickets.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                return false;
            }

            ticket.Title = title;
            ticket.Description = description;
            ticket.TicketType = ticketType;
            ticket.TicketState = ticketState;

            this.db.SaveChanges();

            return true;
        }

        public byte[] GetAttachedFiles(int ticketId)
        {
            Ticket ticket = this.db.Find<Ticket>(ticketId);

            if (ticket == null)
            {
                return null;
            }

            return ticket.AttachedFiles;
        }

        public bool SaveFiles(int ticketId, byte[] attachedFiles)
        {
            Ticket ticket = this.db.Find<Ticket>(ticketId);

            if (ticket == null)
            {
                return false;
            }

            ticket.AttachedFiles = attachedFiles;

            this.db.SaveChanges();

            return true;
        }

        public int Total() => this.db.Tickets.Count();
    }
}
