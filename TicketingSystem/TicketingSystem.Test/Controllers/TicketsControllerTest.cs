namespace TicketingSystem.Test.Controllers
{
    using Data.Models;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Moq;
    using System;
    using Test.Mocks;
    using TicketingSystem.Common.Constants;
    using TicketingSystem.Common.Enums;
    using TicketingSystem.Implementations;
    using TicketingSystem.Services.Admin;
    using TicketingSystem.Services.Tickets;
    using Web;
    using Web.Areas.Tickets.Controllers;
    using Web.Areas.Tickets.Models.Tickets;
    using Xunit;

    public class TicketsControllerTest
    {
        [Fact]
        public void PostCreateShouldReturnRedirectWitValidModel()
        {
            //Arrange
            string modelTitle = null;
            string modelDescription = null;
            DateTime modelPostTime = default(DateTime);
            TicketType modelType = TicketType.BugReport;
            TicketState modelState = TicketState.New;
            string modelSenderId = null;
            int modelProjectId = default(int);

            string successMessage = null;

            var userManager = new Mock<UserService>();

            var ticketService = new Mock<ITicketService>();

            var messageService = new Mock<IMessageService>();

            var adminProjectService = new Mock<IAdminProjectService>();

            ticketService.Setup(t => t.Create(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<TicketType>(),
                It.IsAny<TicketState>(),
                It.IsAny<string>(),
                It.IsAny<int>()))
                .Callback(
                (string title, 
                string description, 
                DateTime postTime, 
                TicketType ticketType, 
                TicketState ticketState, 
                string senderId, 
                int projectId) => 
                {
                    modelTitle = title;
                    modelDescription = description;
                    modelPostTime = postTime;
                    modelType = ticketType;
                    modelState = ticketState;
                    modelSenderId = senderId;
                    modelProjectId = projectId;
                });

            var tempData = new Mock<ITempDataDictionary>();
            tempData.SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new TicketsController(userManager.Object, adminProjectService.Object, ticketService.Object, messageService.Object);
            controller.TempData = tempData.Object;

            //Act
            var result = controller.Create(new SubmitTicketFormModel
            {
                Title = "Testing",
                Description = "Testing the description",
                PostTime = DateTime.UtcNow,
                TicketType = TicketType.BugReport,
                TicketState = TicketState.Running

            });

            //Assert
            modelTitle.Should().Be("Testing");
            modelDescription.Should().Be("Testing the description");
            modelPostTime.Year.Should().Be(2018);
            modelType.Should().Be(TicketType.BugReport);
            modelState.Should().Be(TicketState.Running);

            successMessage.Should().Be($"Ticket Testing successfully sended.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }

        [Fact]
        public void EditShouldReturnNotFoundIfTicketDoesNotExist()
        {
            //Arrange
            var userManager = new Mock<UserService>();

            var ticketService = new Mock<ITicketService>();

            var messageService = new Mock<IMessageService>();

            var adminProjectService = new Mock<IAdminProjectService>();

            var controller = new TicketsController(userManager.Object, adminProjectService.Object, ticketService.Object, messageService.Object);

            //Act
            var result = controller.Edit(1);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
