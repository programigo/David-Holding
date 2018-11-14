using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace TicketingSystem.VueTS.Areas.Admin.Models.Users
{
    public class UserListingModel
    {
        [JsonProperty("users")]
        public IEnumerable<AdminUserListingModel> Users { get; set; }

        [JsonProperty("roles")]
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
