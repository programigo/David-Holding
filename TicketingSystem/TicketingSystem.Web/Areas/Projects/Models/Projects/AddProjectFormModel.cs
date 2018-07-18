namespace TicketingSystem.Web.Areas.Projects.Models.Projects
{
    using Data;
    using System.ComponentModel.DataAnnotations;

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
