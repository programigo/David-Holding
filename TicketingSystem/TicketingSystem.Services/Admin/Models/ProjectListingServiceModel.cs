namespace TicketingSystem.Services.Admin.Models
{
    using Common.Mapping;
    using Data.Models;

    public class ProjectListingServiceModel : IMapFrom<Project>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
    }
}
