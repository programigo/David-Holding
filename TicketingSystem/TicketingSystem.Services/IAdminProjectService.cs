using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Services.Admin.Models;

namespace TicketingSystem.Services.Admin
{
    public interface IAdminProjectService
    {
        IQueryable<ProjectListingServiceModel> All(int page = 1);

        IEnumerable<ProjectListingServiceModel> DropdownAll();

        int Total();

        void Create(string name, string description);

        void Delete(int id);

        bool Edit(int id, string name, string description);

        IQueryable<ProjectListingServiceModel> Details(int id);
    }
}
