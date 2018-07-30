using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using TicketingSystem.Common.Constants;
using TicketingSystem.Common.Enums;
using TicketingSystem.Implementations;
using TicketingSystem.Services.Tickets;
using TicketingSystem.Web.Areas.Tickets.Controllers;
using TicketingSystem.Web.Areas.Tickets.Models.Messages;
using Xunit;

namespace TicketingSystem.Test.Controllers
{
    public class MessagesControllerTest
    {
        [Fact]
        public void PostCreateShouldReturnRedirectWitValidModel()
        {
            //Arrange
            DateTime modelPostDate = default(DateTime);
            string modelAuthorId = null;
            int modelTicketId = default(int);
            MessageState modelState = MessageState.Published;
            string modelContent = null;

            string successMessage = null;

            var userManager = new Mock<UserService>();

            var ticketService = new Mock<ITicketService>();

            var messageService = new Mock<IMessageService>();

            messageService.Setup(t => t.Create(
                It.IsAny<string>(),
                It.IsAny<DateTime>(),
                It.IsAny<MessageState>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
                .Callback(
                (string content,
                DateTime postTime,
                MessageState state,
                int ticketId,
                string authorId
                ) =>
                {
                    modelContent = content;
                    modelPostDate = postTime;
                    modelState = state;
                    modelTicketId = ticketId;
                    modelAuthorId = authorId;
                });

            var tempData = new Mock<ITempDataDictionary>();
            tempData.SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
                .Callback((string key, object message) => successMessage = message as string);

            var controller = new MessagesController(messageService.Object, ticketService.Object, userManager.Object);
            controller.TempData = tempData.Object;

            //Act
            var result = controller.Create(new AddMessageFormModel
            {
                Content = "Some Message Test",
                PostDate = DateTime.UtcNow,
                State = MessageState.Published
            });

            //Assert
            modelContent.Should().Be("Some Message Test");
            modelPostDate.Year.Should().Be(2018);
            modelState.Should().Be(MessageState.Published);

            successMessage.Should().Be($"Message created successfully.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be(nameof(TicketsController.Index), "Tickets");
        }
    }
}
