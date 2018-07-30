using FluentAssertions;
using System;
using TicketingSystem.Common.Enums;
using TicketingSystem.Data.Models;
using TicketingSystem.Implementations;
using Xunit;

namespace TicketingSystem.Test.Services
{
    public class MessageServiceTest
    {
        [Fact]
        public void GetAttachedFilesShouldReturnCorrectResult()
        {
            //Arrange
            var db = Database.GetDatabase();

            var message = new Message
            {
                Id = 1,
                PostDate = DateTime.UtcNow,
                State = MessageState.Published,
                AttachedFiles = new byte[5]
            };

            db.Add(message);

            db.SaveChanges();

            var messageService = new MessageService(db);

            //Act
            var file = messageService.GetAttachedFiles(1);

            //Assert
            file.Length.Should().Be(5);
        }

        [Fact]
        public void SaveFilesShouldReturnTrueForSuccessfullOperation()
        {
            //Arrange
            var db = Database.GetDatabase();

            var message = new Message
            {
                Id = 1,
                PostDate = DateTime.UtcNow,
                State = MessageState.Published
            };

            db.Add(message);

            db.SaveChanges();

            var messageService = new MessageService(db);

            //Act
            bool isAttached = messageService.SaveFiles(1, new byte[7]);

            //Assert
            isAttached.Should().Be(true);
        }
    }
}
