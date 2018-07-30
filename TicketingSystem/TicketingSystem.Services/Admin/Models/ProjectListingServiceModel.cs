using TicketingSystem.Common.Mapping;
using TicketingSystem.Data.Models;

namespace TicketingSystem.Services.Admin.Models
{
    public class ProjectListingServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}
