using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;
using TicketingSystem.VueTS.Common.Enums;

namespace TicketingSystem.VueTS.Areas.Tickets.Models.Messages
{
	public class AddMessageFormModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("postDate")]
		[DataType(DataType.Date)]
		public DateTime PostDate { get; set; }

		[JsonProperty("state")]
		public MessageState State { get; set; }

		[JsonProperty("content")]
		[Required]
		[MinLength(DataConstants.MessageContentMinLength, ErrorMessage = "The Content must be at least {1} characters long.")]
		public string Content { get; set; }

		[JsonProperty("ticketId")]
		[Display(Name = "Ticket")]
		[Required]
		public int TicketId { get; set; }

		[JsonProperty("tickets")]
		public IEnumerable<SelectListItem> Tickets { get; set; }
	}
}
