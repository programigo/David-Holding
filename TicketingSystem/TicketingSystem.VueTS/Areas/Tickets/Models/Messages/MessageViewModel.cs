using Newtonsoft.Json;
using System;
using TicketingSystem.VueTS.Common.Enums;
using TicketingSystem.VueTS.Common.ViewModels;

namespace TicketingSystem.VueTS.Areas.Tickets.Models.Messages
{
	public class MessageViewModel
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("postDate")]
		public DateTime PostDate { get; set; }

		[JsonProperty("authorId")]
		public string AuthorId { get; set; }

		[JsonProperty("author")]
		public string Author { get; set; }

		[JsonProperty("ticketId")]
		public int TicketId { get; set; }

		[JsonProperty("ticket")]
		public Ticket Ticket { get; set; }

		[JsonProperty("state")]
		public MessageState State { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }

		[JsonProperty("attachedFiles")]
		public byte[] AttachedFiles { get; set; }
	}
}
