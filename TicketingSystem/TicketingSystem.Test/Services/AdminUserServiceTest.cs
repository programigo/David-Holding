using FluentAssertions;
using System.Linq;
using TicketingSystem.Data.Models;
using TicketingSystem.Implementations;
using TicketingSystem.Test.Mocks;
using Xunit;

namespace TicketingSystem.Test.Services
{
    public class AdminUserServiceTest
    {
        public AdminUserServiceTest()
        {
            Tests.Initialize();
        }

        [Fact]
        public void RemoveShouldSuccessfullyDeleteUserIfExists()
        {
            //Arrange
            var db = Database.GetDatabase();

            var userManager = UserManagerMock.New.Object;

            var roleManager = RoleManagerMock.New.Object;

            var user = new User { Id = "1a45gty69", UserName = "Krasi", Name = "Krasimir Angelov", Email = "kisamekz@abv.bg" };

            db.Add(user);

            db.SaveChanges();

            var adminUserService = new AdminUserService(db, userManager, roleManager);

            //Act
            adminUserService.Remove("1a45gty69");

            //Assert
            db.Users.Count().Should().Be(0);
        }

        [Fact]
        public void GetUserByNameShouldReturnCorrectUser()
        {
            //Arrange
            var db = Database.GetDatabase();

            var userManager = UserManagerMock.New.Object;

            var roleManager = RoleManagerMock.New.Object;

            var userOne = new User { Id = "1a45gty69", UserName = "Krasi", Name = "Krasimir Angelov", Email = "kisamekz@abv.bg" };
            var userTwo = new User { Id = "5thrdf68", UserName = "Miro", Name = "Miro Iliev", Email = "miro@miro.bg" };

            db.AddRange(userOne, userTwo);

            db.SaveChanges();

            var adminUserService = new AdminUserService(db, userManager, roleManager);

            //Act
            var searchedUser = adminUserService.GetUserByName("Krasi");

            //Assert
            searchedUser.Should().NotBe(null);
        }

        [Fact]
        public void ChangeDataShouldReturnTrueForSuccessfullEdit()
        {
            //Arrange
            var db = Database.GetDatabase();

            var userManager = UserManagerMock.New.Object;

            var roleManager = RoleManagerMock.New.Object;

            var user = new User { Id = "123", UserName = "Ivan", Name = "Ivan Petrov", Email = "ivan@gmail.com" };

            db.Add(user);

            db.SaveChanges();

            var adminUserService = new AdminUserService(db, userManager, roleManager);

            //Act
            var editedUser = adminUserService.ChangeData("123", "Gosho", "gogo@abv.bg");

            //Assert
            editedUser.Should().Be(true);
        }

        [Fact]
        public void IsApprovedUserShouldReturnFalseIfUserIsNull()
        {
            //Arrange
            var db = Database.GetDatabase();

            var userManager = UserManagerMock.New.Object;

            var roleManager = RoleManagerMock.New.Object;

            var user = new User { Id = "158", UserName = "Ivan", Name = "Ivan Petrov", Email = "ivan@gmail.com" };

            db.Add(user);

            db.SaveChanges();

            var adminUserService = new AdminUserService(db, userManager, roleManager);

            //Act
            bool isApproved = adminUserService.IsApprovedUser("Gosho");

            //Assert
            isApproved.Should().Be(false);
        }
    }
}
