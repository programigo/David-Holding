using System;
using System.Collections.Generic;

namespace TicketingSystem.Services
{
	public class AuthenticationProperties
	{
		public IDictionary<string, string> Items { get; set; }

		public bool IsPersistent { get; set; }

		public string RedirectUri { get; set; }

		public DateTimeOffset? IssuedUtc { get; set; }

		public DateTimeOffset? ExpiresUtc { get; set; }

		public bool? AllowRefresh { get; set; }
	}
}
