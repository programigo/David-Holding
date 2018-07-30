using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace TicketingSystem.Implementations
{
    public class RoleService : RoleManager<IdentityRole>
    {
        public RoleService(IRoleStore<IdentityRole> store, IEnumerable<IRoleValidator<IdentityRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<IdentityRole>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        public List<SelectListItem> GetRoles()
        => this.Roles
               .Select(r => new SelectListItem
               {
                   Text = r.Name,
                   Value = r.Name
               })
               .ToList();
    }
}
