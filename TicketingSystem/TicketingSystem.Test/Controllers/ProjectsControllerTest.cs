using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using TicketingSystem.Common.Constants;
using TicketingSystem.Services;
using TicketingSystem.Web.Areas.Projects.Controllers;
using TicketingSystem.Web.Areas.Projects.Models.Projects;
using Xunit;

namespace TicketingSystem.Test.Controllers
{
    public class ProjectsControllerTest
    {
        [Fact]
        public void PostCreateShouldReturnRedirectWitValidModel()
        {
            //Arrange
            string modelName = null;
            string modelDescription = null;

            string successMessage = null;

            var adminProjectService = new Mock<IAdminProjectService>();

            adminProjectService.Setup(t => t.Create(
                It.IsAny<string>(),
                It.IsAny<string>()))
                .Callback(
                (string name,
                string description
                ) =>
                {
                    modelName = name;
                    modelDescription = description;
                });

            var tempData = new Mock<ITempDataDictionary>();
            tempData.SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new ProjectsController(adminProjectService.Object);
            controller.TempData = tempData.Object;

            //Act
            var result = controller.Create(new AddProjectFormModel
            {
                Name = "Some Project",
                Description = "Very cool project. Best of the best."
            });

            //Assert
            modelName.Should().Be("Some Project");
            modelDescription.Should().Be("Very cool project. Best of the best.");

            successMessage.Should().Be($"Project Some Project created successfully");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }

        [Fact]
        public void EditShouldReturnNotFoundIfProjectDoesNotExist()
        {
            //Arrange
            var adminProjectService = new Mock<IAdminProjectService>();

            var controller = new ProjectsController(adminProjectService.Object);

            //Act
            var result = controller.Edit(5);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
