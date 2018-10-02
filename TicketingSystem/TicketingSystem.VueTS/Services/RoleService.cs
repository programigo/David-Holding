using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem.Services;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;
using ServiceIdentityRole = TicketingSystem.Services.IdentityRole;
using SelectListItem = TicketingSystem.Services.SelectListItem;

namespace TicketingSystem.VueTS.Services
{
    public class RoleService : RoleManager<IdentityRole>, IRoleService
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

        Task IRoleService.CreateAsync(ServiceIdentityRole identityRole)
        {
            var systemIdentityRole = new IdentityRole(identityRole.Name); 

            var res = base.CreateAsync(systemIdentityRole);

            return res;
        }
    }
}
