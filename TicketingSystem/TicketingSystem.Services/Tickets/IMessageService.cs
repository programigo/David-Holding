namespace TicketingSystem.Services.Tickets
{
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using Tickets.Models;

    public interface IMessageService
    {
        void Create(string content, DateTime postTime, MessageState state, int ticketId, string authorId);

        IEnumerable<MessageListingServiceModel> All();

        MessageListingServiceModel Details(int id);

        bool SaveFiles(int messageId, byte[] attachedFiles);

        byte[] GetAttachedFiles(int id);
    }
}
