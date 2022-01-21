using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using Municorn.Infrastructure.Commands;
using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Handlers;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Queries;
using Municorn.Infrastructure.Requests;
using Municorn.WebAPI.Controllers.v1;

using Xunit;

namespace Municorn.Tests.WebAPI.IntegrationTests
{
    public class NotificationControllerTests
    {
        [Fact]
        public async Task GetNotificationStatusById_NotificationIsExist_ReturnSuccess()
        {
            // Arrange
            var mediatrMock = new Mock<IMediator>();
            var mockRepo = new Mock<INotificationRepository>();
            var existingNotificationId = Guid.Empty.ToString();
            var status = NotificationStatus.Delivered;
            mockRepo.Setup(repo => repo.GetNotificationStatusByIdAsync(existingNotificationId)).ReturnsAsync(status);
            mediatrMock.Setup(m => m.Send(It.IsAny<GetNotificationStatusQuery>(), It.IsAny<CancellationToken>())).Returns<GetNotificationStatusQuery, CancellationToken>((command, cancellationToken) =>
            {
                var cqHandler = new GetNotificationStatusQueryHandler(mockRepo.Object);
                return cqHandler.Handle(command, cancellationToken);
            });
            var controller = new NotificationController(mediatrMock.Object);

            // Act
            var response = await controller.GetNotificationStatusById(existingNotificationId);

            // Assert
            var viewResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(status, (NotificationStatus)response.Value);
        }

        [Fact]
        public async Task GetNotificationStatusById_NotificationIsNotExist_ReturnError()
        {
            // Arrange
            var mediatrMock = new Mock<IMediator>();
            var mockRepo = new Mock<INotificationRepository>();
            var notExistingNotificationId = Guid.Empty.ToString();
            mockRepo.Setup(repo => repo.GetNotificationStatusByIdAsync(notExistingNotificationId)).Throws(new InvalidOperationException("Ёлемент не найден"));
            mediatrMock.Setup(m => m.Send(It.IsAny<GetNotificationStatusQuery>(), It.IsAny<CancellationToken>())).Returns<GetNotificationStatusQuery, CancellationToken>((command, cancellationToken) =>
            {
                var cqHandler = new GetNotificationStatusQueryHandler(mockRepo.Object);
                return cqHandler.Handle(command, cancellationToken);
            });
            var controller = new NotificationController(mediatrMock.Object);

            // Act
            Func<Task> act = () => controller.GetNotificationStatusById(notExistingNotificationId);

            //Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(act);
            Assert.Equal("Ёлемент не найден", exception.Message);
        }

        [Fact]
        public async Task SaveNotification_AndoroidNotification_Success()
        {
            // Arrange
            var request = new CreateAndroidNotificationRequest() { DeviceToken = "DeviceToken", Message = "Message", Title = "Title" };
            var mediatrMock = new Mock<IMediator>();
            var mockRepo = new Mock<INotificationRepository>();
            var mockManager = new Mock<INotificationManager<CreateAndroidNotificationRequest>>();

            mediatrMock.Setup(m => m.Send(It.IsAny<CreateAndroidNotificationCommand>(), It.IsAny<CancellationToken>())).Returns<CreateAndroidNotificationCommand, CancellationToken>((command, cancellationToken) =>
            {
                var cqHandler = new CreateAndroidNotificationCommandHandler(new Mock<ILogger<CreateAndroidNotificationCommandHandler>>().Object, mockManager.Object, mockRepo.Object);
                return cqHandler.Handle(command, cancellationToken);
            });

            var controller = new NotificationController(mediatrMock.Object);

            // Act
            var response = await controller.CreateAndSendNotification(request);

            //Assert
            var viewResult = Assert.IsType<ObjectResult>(response);
            Assert.Equal(201, response.StatusCode);
        }

    }
}
