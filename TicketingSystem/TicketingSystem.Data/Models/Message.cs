﻿namespace TicketingSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public int Id { get; set; }

        public DateTime PostDate { get; set; }

        public string AuthorId { get; set; }

        public User Author { get; set; }

        public int TicketId { get; set; }

        public Ticket Ticket { get; set; }

        public MessageState State { get; set; }

        [Required]
        [MinLength(DataConstants.MessageContentMinLength)]
        public string Content { get; set; }

        [MaxLength(DataConstants.AttachedFileLength)]
        public byte[] AttachedFiles { get; set; }
    }
}
