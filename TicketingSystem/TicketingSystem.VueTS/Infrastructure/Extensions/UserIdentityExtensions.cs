using System.Security.Claims;

namespace TicketingSystem.VueTS.Infrastructure.Extensions
{
	public static class UserIdentityExtensions
	{
		public static string GetUserId(this ClaimsPrincipal user)
		{
			if (!user.Identity.IsAuthenticated)
				return null;

			ClaimsPrincipal currentUser = user;
			return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
		}
	}
}
