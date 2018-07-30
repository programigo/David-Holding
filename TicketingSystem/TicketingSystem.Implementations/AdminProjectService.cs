using System.Collections.Generic;
using System.Linq;
using TicketingSystem.Data;
using TicketingSystem.Data.Models;
using TicketingSystem.Services.Admin;
using TicketingSystem.Services.Admin.Models;

namespace TicketingSystem.Implementations
{
    public class AdminProjectService : IAdminProjectService
    {
        private readonly TicketingSystemDbContext db;

        public AdminProjectService(TicketingSystemDbContext db)
        {
            this.db = db;
        }

        public IQueryable<ProjectListingServiceModel> All(int page = 1)
        => this.db
            .Projects
            .Skip((page - 1) * 8)
            .Take(8)
            .Select(p => new ProjectListingServiceModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            })
            .AsQueryable();

        public void Create(string name, string description)
        {
            Project project = new Project
            {
                Name = name,
                Description = description
            };

            this.db.Add(project);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            Project project = this.db.Projects.Find(id);

            if (project == null)
            {
                return;
            }

            this.db.Remove(project);

            this.db.SaveChanges();
        }

        public IQueryable<ProjectListingServiceModel> Details(int id)
        => this.db
            .Projects
            .Where(p => p.Id == id)
            .Select(p => new ProjectListingServiceModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            })
            .AsQueryable();

        public IEnumerable<ProjectListingServiceModel> DropdownAll()
        => this.db
            .Projects
            .Select(p => new ProjectListingServiceModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description
            })
            .ToList();

        public bool Edit(int id, string name, string description)
        {
            Project project = this.db.Projects.FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return false;
            }

            project.Name = name;
            project.Description = description;

            this.db.SaveChanges();

            return true;
        }

        public int Total() => this.db.Projects.Count();
    }
}
