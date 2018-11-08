using System;
using System.Linq;

namespace TicketingSystem.Services
{
    public interface IMessageService
    {
        void Create(string content, DateTime postTime, MessageState? state, int ticketId, string authorId);

        IQueryable<MessageListingServiceModel> All();

        IQueryable<MessageListingServiceModel> Details(int id);

        bool SaveFiles(int messageId, byte[] attachedFiles);

        byte[] GetAttachedFiles(int id);
    }
}
