using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Moq;

using Municorn.Infrastructure.Commands;
using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Handlers;
using Municorn.Infrastructure.Requests;
using Municorn.Infrastructure.Services;

using Xunit;

namespace Municorn.Tests.Infrastructure.UnitTests
{
    public class NotificationSenderTests
    {
        [Fact]
        public async Task SendNotificationAsync_ReturnSuccess()
        {
            // Arrange
            var repository = CommonSut.GetInMemoryNotificationRepository();
            var sender = new AndroidNotificationSender(new Mock<ILogger<AndroidNotificationSender>>().Object);
            var handler = new CreateAndroidNotificationCommandHandler(new Mock<ILogger<CreateAndroidNotificationCommandHandler>>().Object, sender, repository);
            var request = new CreateAndroidNotificationRequest() { DeviceToken = "SendNotificationAsync_ReturnSuccess", Message = "Message", Title = "Title" };
            var command = new CreateAndroidNotificationCommand(request);

            // Act
            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            // Assert
            Assert.Equal(request.Id, result.Id);
            Assert.Equal(NotificationStatus.Sent, result.Status);
        }

    }
}
