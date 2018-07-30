using Microsoft.EntityFrameworkCore;
using System;
using TicketingSystem.Data;

namespace TicketingSystem.Test
{
    public static class Database
    {
        public static TicketingSystemDbContext GetDatabase()
        {
            var dbOptions = new DbContextOptionsBuilder<TicketingSystemDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new TicketingSystemDbContext(dbOptions);
        }
    }
}
