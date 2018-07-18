namespace TicketingSystem.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class AdminUserPendingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsApproved { get; set; }
    }
}