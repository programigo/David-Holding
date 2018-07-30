using System;
using System.Collections.Generic;
using TicketingSystem.Common.Enums;
using TicketingSystem.Data.Models;

namespace TicketingSystem.Services.Tickets.Models
{
    public class TicketListingServiceModel
    {
        public int Id { get; set; }

        public DateTime PostTime { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public string Sender { get; set; }

        public string SenderId { get; set; }

        public TicketType TicketType { get; set; }

        public TicketState TicketState { get; set; }
      
        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] AttachedFiles { get; set; }

        public List<MessageListingServiceModel> Messages { get; set; } = new List<MessageListingServiceModel>();
    }
}
