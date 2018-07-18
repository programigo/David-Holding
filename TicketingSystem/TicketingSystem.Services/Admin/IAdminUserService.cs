namespace TicketingSystem.Services.Admin
{
    using Models;
    using System.Collections.Generic;
    using TicketingSystem.Data.Models;

    public interface IAdminUserService
    {
        IEnumerable<AdminUserListingServiceModel> All();

        AdminUserChangeDataServiceModel Details(string id);

        User GetUser(string id);

        User GetUserByName(string username);

        void Approve(string id);

        void Remove(string id);

        bool IsApprovedUser(string username);

        bool ChangeData(string id, string name, string email);

        bool IsAlreadyInRole(string id);
    }
}
