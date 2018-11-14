using Newtonsoft.Json;
using System;

namespace TicketingSystem.VueTS.Models
{
    public class ErrorViewModel
    {
        [JsonProperty("requestId")]
        public string RequestId { get; set; }

        [JsonProperty("showRequestId")]
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}