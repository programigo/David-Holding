using System.Collections.Generic;
using System.Linq;
using TicketingSystem.DatabaseModels;
using TicketingSystem.Services.Admin.Models;

namespace TicketingSystem.Services.Admin
{
    public interface IAdminUserService
    {
        IEnumerable<AdminUserListingServiceModel> All();

        IQueryable<AdminUserListingServiceModel> GetAllUsers();

        IQueryable<AdminUserListingServiceModel> GetPendingUsers();

        IQueryable<AdminUserChangeDataServiceModel> Details(string id);

        IQueryable<UserModel> GetUser(string id);
        
        IQueryable<UserModel> GetUserByName(string username);

        void Approve(string id);

        void Remove(string id);

        bool IsApprovedUser(string username);

        bool ChangeData(string id, string name, string email);

        bool IsAlreadyInRole(string id);
    }
}
