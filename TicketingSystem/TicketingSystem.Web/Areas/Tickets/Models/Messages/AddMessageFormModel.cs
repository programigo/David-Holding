using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;
using TicketingSystem.Web.Common.Enums;

namespace TicketingSystem.Web.Areas.Tickets.Models.Messages
{
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
