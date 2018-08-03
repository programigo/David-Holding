using System;
using System.Linq;

namespace TicketingSystem.Services
{
    public interface ITicketService
    {
        IQueryable<TicketListingServiceModel> All(int page = 1);

        IQueryable<TicketListingServiceModel> DropdownAll();

        int Total();

        void Create(string title, string description, DateTime postTime, TicketType ticketType, TicketState ticketState, string senderId, int projectId);

        bool SaveFiles(int ticketId, byte[] attachedFiles);

        bool Edit(int id, string title, string description, TicketType ticketType, TicketState ticketState);

        IQueryable<TicketListingServiceModel> Details(int id);

        byte[] GetAttachedFiles(int id);

        void Delete(int id);
    }
}
