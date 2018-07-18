namespace TicketingSystem.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Models;
    using System.Collections.Generic;
    using System.Linq;

    public class AdminProjectService : IAdminProjectService
    {
        private readonly TicketingSystemDbContext db;

        public AdminProjectService(TicketingSystemDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<ProjectListingServiceModel> All(int page = 1)
        => this.db
            .Projects
            .Skip((page - 1) * 8)
            .Take(8)
            .ProjectTo<ProjectListingServiceModel>()
            .ToList();

        public void Create(string name, string description)
        {
            var project = new Project
            {
                Name = name,
                Description = description
            };

            this.db.Add(project);
            this.db.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = this.db.Projects.Find(id);

            if (project == null)
            {
                return;
            }

            this.db.Remove(project);

            this.db.SaveChanges();
        }

        public ProjectListingServiceModel Details(int id)
        => this.db
            .Projects
            .Where(p => p.Id == id)
            .ProjectTo<ProjectListingServiceModel>()
            .FirstOrDefault();

        public IEnumerable<ProjectListingServiceModel> DropdownAll()
        => this.db
            .Projects
            .ProjectTo<ProjectListingServiceModel>()
            .ToList();

        public bool Edit(int id, string name, string description)
        {
            var project = this.db.Projects.FirstOrDefault(p => p.Id == id);

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
