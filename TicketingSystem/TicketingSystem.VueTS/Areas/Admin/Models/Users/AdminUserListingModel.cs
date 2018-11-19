using Newtonsoft.Json;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
	public class AdminUserListingModel
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("isApproved")]
		public bool IsApproved { get; set; }
	}
}
