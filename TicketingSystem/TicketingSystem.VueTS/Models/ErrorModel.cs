using Newtonsoft.Json;

namespace TicketingSystem.VueTS.Models
{
    [JsonObject("ErrorModel")]
    public abstract class ErrorModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
