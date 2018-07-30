namespace TicketingSystem.Services.Admin.Models
{
    public class AdminUserPendingServiceModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }
    }
}