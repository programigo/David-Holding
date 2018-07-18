namespace TicketingSystem.Services.Tickets.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class MessageService : IMessageService
    {
        private readonly TicketingSystemDbContext db;

        public MessageService(TicketingSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<MessageListingServiceModel> All()
        => this.db
            .Messages
            .ProjectTo<MessageListingServiceModel>()
            .ToList();

        public void Create(string content, DateTime postTime, MessageState state, int ticketId, string authorId)
        {
            var message = new Message
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

        public MessageListingServiceModel Details(int id)
        => this.db
            .Messages
            .Where(m => m.Id == id)
            .ProjectTo<MessageListingServiceModel>()
            .FirstOrDefault();

        public byte[] GetAttachedFiles(int id)
        {
            var message = this.db.Find<Message>(id);

            if (message == null)
            {
                return null;
            }

            return message.AttachedFiles;
        }

        public bool SaveFiles(int messageId, byte[] attachedFiles)
        {
            var message = this.db.Find<Message>(messageId);

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
