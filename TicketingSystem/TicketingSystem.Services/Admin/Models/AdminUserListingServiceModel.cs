namespace TicketingSystem.Services.Admin.Models
{
    public class AdminUserListingServiceModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }
    }
}