using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;
using TicketingSystem.VueTS.Common.Enums;

namespace TicketingSystem.VueTS.Areas.Tickets.Models.Tickets
{
	public class SubmitTicketFormModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("title")]
		[Required]
		[MinLength(DataConstants.TicketTitleMinLength, ErrorMessage = "The Title must be at least {1} characters long.")]
		[MaxLength(DataConstants.TicketTitleMaxLength, ErrorMessage = "The Description must be maximum {1} characters long.")]
		public string Title { get; set; }

		[JsonProperty("description")]
		[Required]
		[MinLength(DataConstants.TicketDescriptionMinLength, ErrorMessage = "The Description must be at least {1} characters long.")]
		public string Description { get; set; }

		[JsonProperty("postTime")]
		[DataType(DataType.Date)]
		public DateTime PostTime { get; set; }

		[JsonProperty("ticketType")]
		[Display(Name = "Ticket Type")]
		public TicketType TicketType { get; set; }

		[JsonProperty("ticketState")]
		[Display(Name = "Ticket State")]
		public TicketState TicketState { get; set; }

		[JsonProperty("projectId")]
		[Display(Name = "Project")]
		[Required]
		public int ProjectId { get; set; }

		[JsonProperty("projects")]
		public IEnumerable<SelectListItem> Projects { get; set; }
	}
}
