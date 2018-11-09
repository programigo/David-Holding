using Newtonsoft.Json;

namespace TicketingSystem.VueTS.Models
{
    [JsonObject("InternalErrorModel")]
    public class InternalErrorModel: ErrorModel
    {
        [JsonProperty("stackTrace")]
        public string StackTrace { get; set; }

        [JsonProperty("innerError")]
        public InternalErrorModel InnerError { get; set; }
    }
}
