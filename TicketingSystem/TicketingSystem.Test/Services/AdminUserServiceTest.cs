namespace TicketingSystem.Test.Services
{
    using FluentAssertions;
    using Data.Models;
    using TicketingSystem.Services.Admin.Implementations;
    using Xunit;
    using System.Linq;

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

            var user = new User { Id = "1a45gty69", UserName = "Krasi", Name = "Krasimir Angelov", Email = "kisamekz@abv.bg" };

            db.Add(user);

            db.SaveChanges();

            var adminUserService = new AdminUserService(db);

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

            var userOne = new User { Id = "1a45gty69", UserName = "Krasi", Name = "Krasimir Angelov", Email = "kisamekz@abv.bg" };
            var userTwo = new User { Id = "5thrdf68", UserName = "Miro", Name = "Miro Iliev", Email = "miro@miro.bg" };

            db.AddRange(userOne, userTwo);

            db.SaveChanges();

            var adminUserService = new AdminUserService(db);

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

            var user = new User { Id = "123", UserName = "Ivan", Name = "Ivan Petrov", Email = "ivan@gmail.com" };

            db.Add(user);

            db.SaveChanges();

            var adminUserService = new AdminUserService(db);

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

            var user = new User { Id = "158", UserName = "Ivan", Name = "Ivan Petrov", Email = "ivan@gmail.com" };

            db.Add(user);

            db.SaveChanges();

            var adminUserService = new AdminUserService(db);

            //Act
            bool isApproved = adminUserService.IsApprovedUser("Gosho");

            //Assert
            isApproved.Should().Be(false);
        }
    }
}
