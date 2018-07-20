namespace TicketingSystem.Test
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;

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
