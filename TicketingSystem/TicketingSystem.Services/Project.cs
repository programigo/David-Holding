using System.Collections.Generic;

namespace TicketingSystem.Services
{
	public class Project
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public List<Ticket> Tickets { get; set; } = new List<Ticket>();
	}
}