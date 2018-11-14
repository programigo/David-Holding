using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TicketingSystem.VueTS.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BadRequestErrorType
    {
        [JsonProperty("modelState")]
        ModelState,
    }
}