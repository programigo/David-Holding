using System;
using System.Collections.Generic;

namespace TicketingSystem.Web.Areas.Tickets.Models.Tickets
{
    public class TicketListingViewModel
    {
        public IEnumerable<TicketViewModel> Tickets { get; set; }

        public int TotalTickets { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalTickets / 10);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
