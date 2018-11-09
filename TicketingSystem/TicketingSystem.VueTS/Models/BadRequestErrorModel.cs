using Newtonsoft.Json;

namespace TicketingSystem.VueTS.Models
{
    [JsonObject("BadRequestErrorModel")]
    public class BadRequestErrorModel : ErrorModel
    {
        [JsonProperty("type")]
        public BadRequestErrorType Type { get; set; }
    }
}
