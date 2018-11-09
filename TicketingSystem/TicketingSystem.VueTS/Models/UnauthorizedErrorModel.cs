using Newtonsoft.Json;

namespace TicketingSystem.VueTS.Models
{
    [JsonObject("UnauthorizedErrorModel")]
    public class UnauthorizedErrorModel : ErrorModel
    {
        [JsonProperty("type")]
        public UnauthorizedErrorType Type { get; set; }
    }
}