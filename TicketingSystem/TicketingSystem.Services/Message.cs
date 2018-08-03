using System;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Services
{
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
        [MinLength(ServicesDataConstants.MessageContentMinLength)]
        public string Content { get; set; }

        [MaxLength(ServicesDataConstants.AttachedFileLength)]
        public byte[] AttachedFiles { get; set; }
    }
}
