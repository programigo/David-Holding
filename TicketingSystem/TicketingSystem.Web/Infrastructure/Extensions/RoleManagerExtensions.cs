using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace TicketingSystem.Web.Infrastructure.Extensions
{
    public static class RoleManagerExtensions
    {
        public static List<SelectListItem> GetRoles(this RoleManager<IdentityRole> roleManager)
        {
            return roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();
        }
    }
}
