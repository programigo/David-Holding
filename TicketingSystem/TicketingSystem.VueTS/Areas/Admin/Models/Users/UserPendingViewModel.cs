using System.Collections.Generic;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
    public class UserPendingViewModel
    {
        public IEnumerable<AdminUserListingViewModel> Users { get; set; }
    }
}