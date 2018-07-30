﻿using System;
using TicketingSystem.Common.Enums;
using TicketingSystem.Data.Models;

namespace TicketingSystem.Services.Tickets.Models
{
    public class MessageListingServiceModel
    {
        public int Id { get; set; }

        public DateTime PostDate { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public MessageState State { get; set; }

        public string Content { get; set; }

        public byte[] AttachedFiles { get; set; }
    }
}
