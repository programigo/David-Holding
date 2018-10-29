using System;
using System.Collections.Generic;
using TicketingSystem.VueTS.Areas.Tickets.Models.Messages;
using TicketingSystem.VueTS.Common.Enums;

namespace TicketingSystem.VueTS.Areas.Tickets.Models.Tickets
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public DateTime PostTime { get; set; }

        public int ProjectId { get; set; }

        public string Project { get; set; }

        public string Sender { get; set; }

        public string SenderId { get; set; }

        public TicketType TicketType { get; set; }

        public TicketState TicketState { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] AttachedFiles { get; set; }

        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
    }
}
