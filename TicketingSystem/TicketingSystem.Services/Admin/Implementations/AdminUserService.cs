namespace TicketingSystem.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using TicketingSystem.Data.Models;

    public class AdminUserService : IAdminUserService
    {
        private readonly TicketingSystemDbContext db;

        public AdminUserService(TicketingSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AdminUserListingServiceModel> All()
            => this.db
            .Users
            .ProjectTo<AdminUserListingServiceModel>()
            .ToList();

        public void Approve(string id)
        {
            var user = this.db.Users.Find(id);

            if (user == null)
            {
                return;
            }

            user.IsApproved = true;

            this.db.SaveChanges();
        }

        public bool ChangeData(string id, string name, string email)
        {
            var user = this.db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return false;
            }

            user.Name = name;
            user.Email = email;

            this.db.SaveChanges();

            return true;
        }

        public AdminUserChangeDataServiceModel Details(string id)
        => this.db
            .Users
            .Where(u => u.Id == id)
            .ProjectTo<AdminUserChangeDataServiceModel>()
            .FirstOrDefault();

        public User GetUser(string id)
        => this.db
            .Users
            .Where(u => u.Id == id)
            .FirstOrDefault();

        public User GetUserByName(string username)
        => this.db
            .Users
            .Where(u => u.UserName == username)
            .FirstOrDefault();

        public bool IsAlreadyInRole(string id)
        {
            var userHasRole = this.db.UserRoles.FirstOrDefault(u => u.UserId == id) != null;

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
            var user = this.db.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return false;
            }

            return user.IsApproved;
        }

        public void Remove(string id)
        {
            var user = this.db.Users.Find(id);

            if (user == null)
            {
                return;
            }

            this.db.Remove(user);

            this.db.SaveChanges();
        }
    }
}