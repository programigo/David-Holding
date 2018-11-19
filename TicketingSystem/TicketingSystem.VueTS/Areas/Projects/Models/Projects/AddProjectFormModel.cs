using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;

namespace TicketingSystem.VueTS.Areas.Projects.Models.Projects
{
	public class AddProjectFormModel
	{
		[JsonProperty("name")]
		[Required]
		[MinLength(DataConstants.ProjectNameMinLength, ErrorMessage = "The Name must be at least {1} characters long.")]
		[MaxLength(DataConstants.ProjectNameMaxLength, ErrorMessage = "The Name must be maximum {1} characters long.")]
		public string Name { get; set; }

		[JsonProperty("description")]
		[Required]
		[MinLength(DataConstants.ProjectDescriptionMinLength, ErrorMessage = "The Description must be at least {1} characters long.")]
		public string Description { get; set; }
	}
}
