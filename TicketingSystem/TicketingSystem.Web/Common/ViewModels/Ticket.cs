﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Common.Constants;
using TicketingSystem.Web.Common.Enums;

namespace TicketingSystem.Web.Common.ViewModels
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

        [Required]
        [MinLength(DataConstants.TicketTitleMinLength)]
        [MaxLength(DataConstants.TicketTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(DataConstants.TicketDescriptionMinLength)]
        public string Description { get; set; }

        [MaxLength(DataConstants.AttachedFileLength)]
        public byte[] AttachedFiles { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}