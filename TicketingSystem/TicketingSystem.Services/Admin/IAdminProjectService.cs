namespace TicketingSystem.Services.Admin
{
    using Models;
    using System.Collections.Generic;

    public interface IAdminProjectService
    {
        IEnumerable<ProjectListingServiceModel> All(int page = 1);

        IEnumerable<ProjectListingServiceModel> DropdownAll();

        int Total();

        void Create(string name, string description);

        void Delete(int id);

        bool Edit(int id, string name, string description);

        ProjectListingServiceModel Details(int id);
    }
}
