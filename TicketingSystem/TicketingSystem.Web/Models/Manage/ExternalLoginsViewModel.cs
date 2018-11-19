using System.Collections.Generic;
using TicketingSystem.Services;

namespace TicketingSystem.Web.Models.ManageViewModels
{
	public class ExternalLoginsViewModel
	{
		public IList<UserLoginInfo> CurrentLogins { get; set; }

		public IList<AuthenticationScheme> OtherLogins { get; set; }

		public bool ShowRemoveButton { get; set; }

		public string StatusMessage { get; set; }
	}
}
