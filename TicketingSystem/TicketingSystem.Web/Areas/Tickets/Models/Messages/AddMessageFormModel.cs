namespace TicketingSystem.Web.Areas.Tickets.Models.Messages
{
    using Data;
    using Data.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddMessageFormModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }

        public MessageState State { get; set; }

        [Required]
        [MinLength(DataConstants.MessageContentMinLength)]
        public string Content { get; set; }

        [Display(Name = "Ticket")]
        [Required]
        public int TicketId { get; set; }

        public IEnumerable<SelectListItem> Tickets { get; set; }
    }
}
