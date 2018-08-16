using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketingSystem.Services;

namespace TicketingSystem.Web.Services
{
    public class RoleService :  IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public List<SelectListItem> GetRoles()
        => roleManager.Roles
               .Select(r => new SelectListItem
               {
                   Text = r.Name,
                   Value = r.Name
               })
               .ToList();

        public Task<bool> RoleExistsAsync(string role)
        => roleManager.RoleExistsAsync(role);

        Task IRoleService.CreateAsync(IdentityRole identityRole)
        => roleManager.CreateAsync(identityRole);
    }
}
