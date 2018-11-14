using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TicketingSystem.VueTS.Areas.Tickets.Models.Messages;
using TicketingSystem.VueTS.Common.Enums;

namespace TicketingSystem.VueTS.Areas.Tickets.Models.Tickets
{
    public class TicketViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("postTime")]
        public DateTime PostTime { get; set; }

        [JsonProperty("projectId")]
        public int ProjectId { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("senderId")]
        public string SenderId { get; set; }

        [JsonProperty("ticketType")]
        public TicketType TicketType { get; set; }

        [JsonProperty("ticketState")]
        public TicketState TicketState { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("attachedFiles")]
        public byte[] AttachedFiles { get; set; }

        [JsonProperty("messages")]
        public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
    }
}
