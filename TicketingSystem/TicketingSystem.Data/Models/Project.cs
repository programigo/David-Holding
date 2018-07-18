namespace TicketingSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DataConstants.ProjectNameMinLength)]
        [MaxLength(DataConstants.ProjectNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DataConstants.ProjectDescriptionMinLength)]
        public string Description { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
