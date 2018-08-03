using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Services
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MinLength(ServicesDataConstants.ProjectNameMinLength)]
        [MaxLength(ServicesDataConstants.ProjectNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(ServicesDataConstants.ProjectDescriptionMinLength)]
        public string Description { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}