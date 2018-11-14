using Newtonsoft.Json;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
    public class UserChangeDataModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
