using System.Collections.Generic;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
    public class UserPendingModel
    {
        public IEnumerable<AdminUserListingModel> Users { get; set; }
    }
}