using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;

namespace TicketingSystem.VueTS.Areas.Projects.Models.Projects
{
    public class AddProjectFormModel
    {
        [Required]
        [MinLength(DataConstants.ProjectNameMinLength)]
        [MaxLength(DataConstants.ProjectNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MinLength(DataConstants.ProjectDescriptionMinLength)]
        public string Description { get; set; }
    }
}
