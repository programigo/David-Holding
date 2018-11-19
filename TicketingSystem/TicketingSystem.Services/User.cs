using System.Collections.Generic;

namespace TicketingSystem.Services
{
	public class User
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Email { get; set; }

		public List<Ticket> Tickets { get; set; } = new List<Ticket>();

		public bool IsApproved { get; set; }

		public string UserName { get; set; }

		public string SecurityStamp { get; set; }

		public string NormalizedEmail { get; set; }

		public string NormalizedUserName { get; set; }

		public string PasswordHash { get; set; }
		public string PhoneNumber { get; set; }
		public bool EmailConfirmed { get; set; }
		public bool TwoFactorEnabled { get; set; }
	}
}
