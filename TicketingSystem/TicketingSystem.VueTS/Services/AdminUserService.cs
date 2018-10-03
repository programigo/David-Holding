using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Services;
using TicketingSystem.VueTS.Common.Constants;
using DATA = TicketingSystem.Data;
using DATA_MODELS = TicketingSystem.Data.Models;

namespace TicketingSystem.VueTS.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly DATA.TicketingSystemDbContext db;
        
        public AdminUserService(DATA.TicketingSystemDbContext db)
        {
            this.db = db;
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
            DATA_MODELS.User user = this.db.Users.Find(id);

            if (user == null)
            {
                return;
            }

            user.IsApproved = true;

            this.db.SaveChanges();
        }

        public bool ChangeData(string id, string name, string email)
        {
            DATA_MODELS.User user = this.db.Users.FirstOrDefault(u => u.Id == id);

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

        public IQueryable<User> GetUser(string id)
        => this.db
            .Users
            .Where(u => u.Id == id)
            .Select(u => new User
            {
                Id = u.Id,
                UserName = u.UserName,
                Name = u.Name,
                Email = u.Email,
                IsApproved = u.IsApproved
            })
            .AsQueryable();

        public IQueryable<User> GetUserByName(string username)
        => this.db
            .Users
            .Where(u => u.UserName == username)
            .Select(u => new User
            {
                Id = u.Id,
                UserName = u.UserName,
                Name = u.Name,
                Email = u.Email,
                IsApproved = u.IsApproved
            })
            .AsQueryable();

        private List<Ticket> GetUserTickets(string id)
        => this.db
            .Tickets
            .Where(t => t.SenderId == id)
            .Select(t => new Ticket
            {
                Id = t.Id,
                PostTime = t.PostTime,
                ProjectId = t.ProjectId,
                SenderId = t.SenderId,
                TicketType = (TicketType)Enum.Parse(typeof(TicketType), t.TicketType.ToString()),
                TicketState = (TicketState)Enum.Parse(typeof(TicketState), t.TicketState.ToString()),
                Title = t.Title,
                Description = t.Description,
                AttachedFiles = t.AttachedFiles
            })
            .ToList();

        private User GetSender(string id)
        => this.db
                .Users
                .Where(u => u.Id == id)
                .Select(u => new User
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Name = u.Name,
                    Email = u.Email,
                    IsApproved = u.IsApproved
                })
                .FirstOrDefault();

        private Project GetProject(int id)
        => this.db
                .Projects
                .Where(p => p.Id == id)
                .Select(p => new Project
                {
                    Name = p.Name,
                    Description = p.Description
                })
                .FirstOrDefault();

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
            DATA_MODELS.User user = this.db.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return false;
            }

            return user.IsApproved;
        }

        public void Remove(string id)
        {
            DATA_MODELS.User user = this.db.Users.Find(id);

            if (user == null)
            {
                return;
            }

            this.db.Remove(user);

            this.db.SaveChanges();
        }
    }
}