using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TicketingSystem.VueTS.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnauthorizedErrorType
    {
        NoSuchUser,
        NotApproved,
        WrongPassword,
        MustChangePassword,
        PasswordHasExpired,
        InvalidLogOnCredentials,
    }
}
