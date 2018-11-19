using System.Collections.Generic;
using System.Threading.Tasks;

namespace TicketingSystem.Services
{
	public interface IRoleService
	{
		IEnumerable<SelectListItem> GetRoles();

		Task<bool> RoleExistsAsync(string role);

		Task CreateAsync(IdentityRole identityRole);
	}
}
