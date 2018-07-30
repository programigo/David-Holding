namespace TicketingSystem.Web.Areas.Admin.Models.Users
{
    public class AdminUserListingViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }
    }
}
