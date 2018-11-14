using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TicketingSystem.VueTS.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnauthorizedErrorType
    {
        [JsonProperty("noSuchUser")]
        NoSuchUser,

        [JsonProperty("notApproved")]
        NotApproved,

        [JsonProperty("wrongPassword")]
        WrongPassword,

        [JsonProperty("mustChangePassword")]
        MustChangePassword,

        [JsonProperty("passwordHasExpired")]
        PasswordHasExpired,

        [JsonProperty("invalidLogOnCredentials")]
        InvalidLogOnCredentials,
    }
}
