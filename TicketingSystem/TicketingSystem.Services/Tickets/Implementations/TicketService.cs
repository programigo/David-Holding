namespace TicketingSystem.Services.Tickets.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TicketService : ITicketService
    {
        private readonly TicketingSystemDbContext db;
        private readonly UserManager<User> userManager;

        public TicketService(TicketingSystemDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IEnumerable<TicketListingServiceModel> All(int page = 1)
        => this.db
            .Tickets
            .Skip((page - 1) * 10)
            .Take(10)
            .ProjectTo<TicketListingServiceModel>()
            .ToList();
        
        public void Create(string title, string description, DateTime postTime, TicketType ticketType, TicketState ticketState, string senderId, int projectId)
        {
            var ticket = new Ticket
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
            var ticket = this.db.Tickets.Find(id);

            if (ticket == null)
            {
                return;
            }

            this.db.Remove(ticket);

            this.db.SaveChanges();
        }

        public TicketListingServiceModel Details(int id)
        => this.db
            .Tickets
            .Where(t => t.Id == id)
            .ProjectTo<TicketListingServiceModel>()
            .FirstOrDefault();

        public IEnumerable<TicketListingServiceModel> DropdownAll()
        => this.db
            .Tickets
            .ProjectTo<TicketListingServiceModel>()
            .ToList();

        public bool Edit(int id, TicketType ticketType, TicketState ticketState)
        {
            var ticket = this.db.Tickets.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                return false;
            }

            ticket.TicketType = ticketType;
            ticket.TicketState = ticketState;

            this.db.SaveChanges();

            return true;
        }

        public byte[] GetAttachedFiles(int ticketId)
        {
            var ticket = this.db.Find<Ticket>(ticketId);

            if (ticket == null)
            {
                return null;
            }

            return ticket.AttachedFiles;
        }

        public bool SaveFiles(int ticketId, byte[] attachedFiles)
        {
            var ticket = this.db.Find<Ticket>(ticketId);

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
