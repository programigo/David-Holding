using System;
using System.Collections.Generic;

namespace TicketingSystem.Web.Areas.Projects.Models.Projects
{
    public class ProjectListingViewModel
    {
        public IEnumerable<ProjectViewModel> Projects { get; set; }

        public int TotalProjects { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalProjects / 8);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
