namespace TicketingSystem.Services.Tickets
{
    using Data.Models;
    using Models;
    using System;
    using System.Collections.Generic;

    public interface ITicketService
    {
        IEnumerable<TicketListingServiceModel> All(int page = 1);

        IEnumerable<TicketListingServiceModel> DropdownAll();

        int Total();

        void Create(string title, string description, DateTime postTime, TicketType ticketType, TicketState ticketState, string senderId, int projectId);

        bool SaveFiles(int ticketId, byte[] attachedFiles);

        bool Edit(int id, string title, string description, TicketType ticketType, TicketState ticketState);

        TicketListingServiceModel Details(int id);

        byte[] GetAttachedFiles(int id);

        void Delete(int id);
    }
}
