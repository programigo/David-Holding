using System;
using System.Collections.Generic;

namespace TicketingSystem.Services
{
    public class Ticket
    {
        public int Id { get; set; }

        public DateTime PostTime { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public TicketType TicketType { get; set; }

        public TicketState TicketState { get; set; }
       
        public string Title { get; set; }

        public string Description { get; set; }

        public byte[] AttachedFiles { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}