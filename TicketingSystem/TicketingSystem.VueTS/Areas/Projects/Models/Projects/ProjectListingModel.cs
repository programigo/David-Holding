using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace TicketingSystem.VueTS.Areas.Projects.Models.Projects
{
    public class ProjectListingModel
    {
        [JsonProperty("projects")]
        public IEnumerable<ProjectModel> Projects { get; set; }

        [JsonProperty("totalProjects")]
        public int TotalProjects { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages => (int)Math.Ceiling((double)this.TotalProjects / 8);

        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }
    }
}
