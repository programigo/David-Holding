﻿using System.Collections.Generic;
using TicketingSystem.Data.Models;

namespace TicketingSystem.DatabaseModels
{
    public class UserModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public bool IsApproved { get; set; }
    }
}