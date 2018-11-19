using Newtonsoft.Json;

namespace TicketingSystem.VueTS.Areas.Projects.Models.Projects
{
	public class ProjectModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
