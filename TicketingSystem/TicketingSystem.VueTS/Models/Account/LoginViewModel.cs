using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.VueTS.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [JsonProperty("username")]
        [Required]
        public string Username { get; set; }

        [JsonProperty("password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
