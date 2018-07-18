namespace TicketingSystem.Web.Areas.Admin.Models.Users
{
    using Services.Admin.Models;
    using System.Collections.Generic;

    public class UserPendingViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }
    }
}