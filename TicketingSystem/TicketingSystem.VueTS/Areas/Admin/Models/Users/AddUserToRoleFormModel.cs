using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
	public class AddUserToRoleFormModel
	{
		[JsonProperty("userId")]
		[Required]
		public string UserId { get; set; }

		[JsonProperty("role")]
		[Required]
		public string Role { get; set; }
	}
}
