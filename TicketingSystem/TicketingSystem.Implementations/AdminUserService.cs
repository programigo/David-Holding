using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Common.Constants;
using TicketingSystem.Data;
using TicketingSystem.Data.Models;
using TicketingSystem.DatabaseModels;
using TicketingSystem.Services.Admin;
using TicketingSystem.Services.Admin.Models;

namespace TicketingSystem.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly TicketingSystemDbContext db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminUserService(TicketingSystemDbContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IEnumerable<AdminUserListingServiceModel> All()
            => this.db
            .Users
            .Select(u => new AdminUserListingServiceModel
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                IsApproved = u.IsApproved
            })
            .ToList();

        public void Approve(string id)
        {
            User user = this.db.Users.Find(id);

            if (user == null)
            {
                return;
            }

            user.IsApproved = true;

            this.db.SaveChanges();
        }

        public bool ChangeData(string id, string name, string email)
        {
            User user = this.db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            user.Name = name;
            user.Email = email;

            this.db.SaveChanges();

            return true;
        }

        public IQueryable<AdminUserChangeDataServiceModel> Details(string id)
        => this.db
            .Users
            .Where(u => u.Id == id)
            .Select(u => new AdminUserChangeDataServiceModel
            {
                Id = u.Id,
                Username = u.UserName,
                Name = u.Name,
                Email = u.Email
            })
            .AsQueryable();

        public IQueryable<UserModel> GetUser(string id)
        => this.db
            .Users
            .Where(u => u.Id == id)
            .Select(u => new UserModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Name = u.Name,
                Email = u.Email,
                Tickets = u.Tickets,
                IsApproved = u.IsApproved
            })
            .AsQueryable();
        
        public IQueryable<UserModel> GetUserByName(string username)
        => this.db
            .Users
            .Where(u => u.UserName == username)
            .Select(u => new UserModel
            {
                Id = u.Id,
                UserName = u.UserName,
                Name = u.Name,
                Email = u.Email,
                Tickets = u.Tickets,
                IsApproved = u.IsApproved
            })
            .AsQueryable();

        public IQueryable<AdminUserListingServiceModel> GetAllUsers()
          => this.All()
            .Where(
                u => u.IsApproved == true &&
                u.Username != WebConstants.AdministratorRole)
            .AsQueryable();

        public IQueryable<AdminUserListingServiceModel> GetPendingUsers()
        =>
             this.All().Where(
                u => u.IsApproved == false &&
                u.Username != WebConstants.AdministratorRole)
            .AsQueryable();
        

        public bool IsAlreadyInRole(string id)
        {
            bool userHasRole = this.db.UserRoles.FirstOrDefault(u => u.UserId == id) != null;

            if (userHasRole)
            {
                var user = this.db.UserRoles.FirstOrDefault(u => u.UserId == id);
                this.db.UserRoles.Remove(user);
                this.db.SaveChanges();

                return true;
            }

            return false;
        }

        public bool IsApprovedUser(string username)
        {
            User user = this.db.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return false;
            }

            return user.IsApproved;
        }

        public void Remove(string id)
        {
            User user = this.db.Users.Find(id);

            if (user == null)
            {
                return;
            }

            this.db.Remove(user);

            this.db.SaveChanges();
        }
    }
}