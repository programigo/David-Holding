using FluentAssertions;
using System.Linq;
using TicketingSystem.Data.Models;
using TicketingSystem.Implementations;
using Xunit;

namespace TicketingSystem.Test.Services
{
    public class AdminProjectServiceTest
    {
        [Fact]
        public void EditShouldReturnTrueForSuccessfullResult()
        {
            //Arrange
            var db = Database.GetDatabase();

            var project = new Project { Name = "Making new processor", Description = "The processor will have 9 cores and 16 threads." };

            db.Add(project);

            db.SaveChanges();

            var projectService = new AdminProjectService(db);

            //Act
            var editedProject = projectService.Edit(1, "Another processor", "Same as above");

            //Assert
            editedProject.Should().Be(true);
        }

        [Fact]
        public void DeleteShouldNotMakeChangesIfProjectDoesNotExist()
        {
            //Arrange
            var db = Database.GetDatabase();

            var projectOne = new Project { Id = 1, Name = "Making new processor", Description = "The processor will have 9 cores and 16 threads." };
            var projectTwo = new Project { Id = 2, Name = "Making new AMD processor", Description = "Good for gaming." };

            db.AddRange(projectOne, projectTwo);

            db.SaveChanges();

            var projectService = new AdminProjectService(db);

            //Act
            projectService.Delete(3);

            //Assert
            db.Projects.Count().Should().Be(2);
        }
    }
}
