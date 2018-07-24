namespace TicketingSystem.Test.Services
{
    using Data.Models;
    using FluentAssertions;
    using System;
    using System.Linq;
    using TicketingSystem.Services.Tickets.Implementations;
    using TicketingSystem.Test.Mocks;
    using Xunit;

    public class TicketServiceTest
    {
        [Fact]
        public void GetAttachedFilesShouldReturnCorrectResult()
        {
            //Arrange
            var db = Database.GetDatabase();

            var userManager = UserManagerMock.New.Object;

            var ticket = new Ticket
            {
                Id = 1,
                PostTime = DateTime.UtcNow,
                TicketType = TicketType.BugReport,
                TicketState = TicketState.New,
                AttachedFiles = new byte[10]
            };

            db.Add(ticket);

            db.SaveChanges();

            var ticketService = new TicketService(db, userManager);

            //Act
            var file = ticketService.GetAttachedFiles(1);

            //Assert
            file.Length.Should().Be(10);
        }

        [Fact]
        public void SaveFilesShouldReturnTrueForSuccessfullOperation()
        {
            //Arrange
            var db = Database.GetDatabase();

            var userManager = UserManagerMock.New.Object;

            var ticket = new Ticket
            {
                Id = 1,
                PostTime = DateTime.UtcNow,
                TicketType = TicketType.BugReport,
                TicketState = TicketState.New
            };

            db.Add(ticket);

            db.SaveChanges();

            var ticketService = new TicketService(db, userManager);

            //Act
            bool isAttached = ticketService.SaveFiles(1, new byte[8]);

            //Assert
            isAttached.Should().Be(true);
        }

        [Fact]
        public void DeleteShouldRemoveSuccessfullyWithValidId()
        {
            //Arrange
            var db = Database.GetDatabase();

            var userManager = UserManagerMock.New.Object;

            var ticketOne = new Ticket
            {
                Id = 2,
                PostTime = DateTime.Now,
                TicketType = TicketType.BugReport,
                TicketState = TicketState.New
            };

            var ticketTwo = new Ticket
            {
                Id = 3,
                PostTime = DateTime.Now,
                TicketType = TicketType.BugReport,
                TicketState = TicketState.New
            };

            db.AddRange(ticketOne, ticketTwo);

            db.SaveChanges();

            var ticketService = new TicketService(db, userManager);

            //Act
            ticketService.Delete(2);

            //Assert
            db.Tickets.Count().Should().Be(1);
        }

        [Fact]
        public void EditShouldReturnTrueForSuccess()
        {
            //Arrange
            var db = Database.GetDatabase();

            var userManager = UserManagerMock.New.Object;

            var ticketOne = new Ticket
            {
                Id = 1,
                PostTime = DateTime.Now,
                TicketState = TicketState.Running,
                TicketType = TicketType.BugReport
            };

            var ticketTwo = new Ticket
            {
                Id = 2,
                Title = "Testing",
                Description = "Again testing",
                PostTime = DateTime.Now.AddHours(2),
                TicketState = TicketState.New,
                TicketType = TicketType.AssistanceRequest
            };

            db.AddRange(ticketOne, ticketTwo);

            db.SaveChanges();

            var ticketService = new TicketService(db, userManager);

            //Act
            var isEditedTicket = ticketService.Edit(1, "Some ticket title", "ticket description", TicketType.BugReport, TicketState.New);

            //Assert
            isEditedTicket.Should().Be(true);
        }
    }
}
