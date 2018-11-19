using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TicketingSystem.Data.Constants;
using TicketingSystem.Web.Common.Enums;

namespace TicketingSystem.Web.Areas.Tickets.Models.Tickets
{
	public class SubmitTicketFormModel
	{
		public int Id { get; set; }

		[Required]
		[MinLength(DataConstants.TicketTitleMinLength)]
		[MaxLength(DataConstants.TicketTitleMaxLength)]
		public string Title { get; set; }

		[Required]
		[MinLength(DataConstants.TicketDescriptionMinLength)]
		public string Description { get; set; }

		[DataType(DataType.Date)]
		public DateTime PostTime { get; set; }

		[Display(Name = "Ticket Type")]
		public TicketType TicketType { get; set; }

		[Display(Name = "Ticket State")]
		public TicketState TicketState { get; set; }

		[Display(Name = "Project")]
		[Required]
		public int ProjectId { get; set; }

		public IEnumerable<SelectListItem> Projects { get; set; }
	}
}
