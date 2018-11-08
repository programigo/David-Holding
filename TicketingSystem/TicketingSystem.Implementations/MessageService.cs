using System;
using System.Linq;
using TicketingSystem.Data;
using DATA_MODELS = TicketingSystem.Data.Models;
using DATA_ENUMS = TicketingSystem.Data.Enums;
using TicketingSystem.Services;

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
                Content = m.Content,
                AttachedFiles = m.AttachedFiles
            })
            .AsQueryable();

        public void Create(string content, DateTime postTime, MessageState? state, int ticketId, string authorId)
        {
            DATA_MODELS.Message message = new DATA_MODELS.Message
            {
                Content = content,
                PostDate = postTime,
                State = state != null ? (DATA_ENUMS.MessageState)Enum.Parse(typeof(DATA_ENUMS.MessageState), state.ToString()) : default(DATA_ENUMS.MessageState),
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
                Content = m.Content,
                AttachedFiles = m.AttachedFiles
            })
            .AsQueryable();

        public byte[] GetAttachedFiles(int id)
        {
            DATA_MODELS.Message message = this.db.Find<DATA_MODELS.Message>(id);

            if (message == null)
            {
                return null;
            }

            return message.AttachedFiles;
        }

        public bool SaveFiles(int messageId, byte[] attachedFiles)
        {
            DATA_MODELS.Message message = this.db.Find<DATA_MODELS.Message>(messageId);

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
