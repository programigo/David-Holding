using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
    public class AdminChangeDataModel
    {
        public string Id { get; set; }

        [JsonProperty("username")]
        [Required]
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        public string Username { get; set; }

        [JsonProperty("name")]
        [Required]
        [MinLength(DataConstants.UserNameMinLength)]
        [MaxLength(DataConstants.UserNameMaxLength)]
        public string Name { get; set; }

        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }
    }
}
