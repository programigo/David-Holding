using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
    public class UserListingModel
    {
        public IEnumerable<AdminUserListingModel> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
