using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TicketingSystem.VueTS.Areas.Tickets.Models.Tickets
{
    public class TicketListingViewModel
    {
        [JsonProperty("tickets")]
        public IEnumerable<TicketViewModel> Tickets { get; set; }

        [JsonProperty("totalTickets")]
        public int TotalTickets { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages => (int)Math.Ceiling((double)this.TotalTickets / 10);

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }
    }
}
