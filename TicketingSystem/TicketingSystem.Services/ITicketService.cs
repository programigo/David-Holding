using System;
using System.Linq;

namespace TicketingSystem.Services
{
	public interface ITicketService
	{
		IQueryable<TicketListingServiceModel> All(int page = 1);

		IQueryable<TicketListingServiceModel> GetAllTickets();

		IQueryable<TicketListingServiceModel> DropdownAll();

		IQueryable<TicketListingServiceModel> Details(int id);

		void Create(string title, string description, DateTime postTime, TicketType ticketType, TicketState? ticketState, string senderId, int projectId);

		bool Edit(int id, string title, string description, TicketType ticketType, TicketState ticketState);

		bool SaveFiles(int ticketId, byte[] attachedFiles);

		byte[] GetAttachedFiles(int id);

		void Delete(int id);

		int Total();
	}
}
