using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Moq;

using Municorn.Infrastructure.Commands;
using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Handlers;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Requests;
using Municorn.Infrastructure.Services;

using Xunit;

namespace Municorn.Tests.Infrastructure.UnitTests
{
    public class AndroidNotificationManagerTests
    {
        [Fact]
        public async Task SendNotificationAsync_ReturnSuccess()
        {
            // Arrange
            var mockRepo = new Mock<INotificationRepository>();
            var mockManager = new Mock<INotificationManager<CreateAndroidNotificationRequest>>();
            var handler = new CreateAndroidNotificationCommandHandler(new Mock<ILogger<CreateAndroidNotificationCommandHandler>>().Object, mockManager.Object, mockRepo.Object);
            var request = new CreateAndroidNotificationRequest() { DeviceToken = "DeviceToken", Message = "Message", Title = "Title" };
            var command = new CreateAndroidNotificationCommand(request);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());
            // Act


            // Assert
            //  Assert.Equal(NotificationStatus.Sent, response);
        }
    }
}
