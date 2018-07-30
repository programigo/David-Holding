using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketingSystem.Data.Models;

namespace TicketingSystem.Data
{
    public class TicketingSystemDbContext : IdentityDbContext<User>
    {
        public TicketingSystemDbContext(DbContextOptions<TicketingSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(u => u.Tickets)
                .WithOne(t => t.Sender)
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<Project>()
                .HasMany(p => p.Tickets)
                .WithOne(t => t.Project)
                .HasForeignKey(t => t.ProjectId);

            builder
                .Entity<Ticket>()
                .HasOne(t => t.Sender)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<Message>()
                .HasOne(m => m.Ticket)
                .WithMany(t => t.Messages)
                .HasForeignKey(m => m.TicketId);

            base.OnModelCreating(builder);
        }
    }
}
