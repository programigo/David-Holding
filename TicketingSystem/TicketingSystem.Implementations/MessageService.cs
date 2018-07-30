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
    public class MessageService : IMessageService
    {
        private readonly TicketingSystemDbContext db;

        public MessageService(TicketingSystemDbContext db)
        {
            this.db = db;
        }

        public IQueryable<MessageListingServiceModel> All()
        => this.db
            .Messages
            .Select(m => new MessageListingServiceModel
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
            })
            .AsQueryable();

        public void Create(string content, DateTime postTime, MessageState state, int ticketId, string authorId)
        {
            Message message = new Message
            {
                Content = content,
                PostDate = postTime,
                State = state,
                TicketId = ticketId,
                AuthorId = authorId
            };

            this.db.Add(message);

            this.db.SaveChanges();
        }

        public IQueryable<MessageListingServiceModel> Details(int id)
        => this.db
            .Messages
            .Where(m => m.Id == id)
            .Select(m => new MessageListingServiceModel
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
            })
            .AsQueryable();

        public byte[] GetAttachedFiles(int id)
        {
            Message message = this.db.Find<Message>(id);

            if (message == null)
            {
                return null;
            }

            return message.AttachedFiles;
        }

        public bool SaveFiles(int messageId, byte[] attachedFiles)
        {
            Message message = this.db.Find<Message>(messageId);

            if (message == null)
            {
                return false;
            }

            message.AttachedFiles = attachedFiles;

            this.db.SaveChanges();

            return true;
        }
    }
}
