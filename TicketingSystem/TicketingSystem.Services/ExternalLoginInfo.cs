using System.Security.Claims;

namespace TicketingSystem.Services
{
	public class ExternalLoginInfo : UserLoginInfo
	{
		public ExternalLoginInfo(ClaimsPrincipal principal, string loginProvider, string providerKey, string displayName) : base(loginProvider, providerKey, displayName)
		{
			this.Principal = principal;
		}

		public ClaimsPrincipal Principal { get; set; }
	}
}
