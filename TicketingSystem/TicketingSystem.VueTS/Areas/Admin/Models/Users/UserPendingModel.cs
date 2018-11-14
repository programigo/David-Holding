using Newtonsoft.Json;
using System.Collections.Generic;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
    public class UserPendingModel
    {
        [JsonProperty("users")]
        public IEnumerable<AdminUserListingModel> Users { get; set; }
    }
}