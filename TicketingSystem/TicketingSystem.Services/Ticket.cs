using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        [MinLength(ServicesDataConstants.TicketTitleMinLength)]
        [MaxLength(ServicesDataConstants.TicketTitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(ServicesDataConstants.TicketDescriptionMinLength)]
        public string Description { get; set; }

        [MaxLength(ServicesDataConstants.AttachedFileLength)]
        public byte[] AttachedFiles { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}