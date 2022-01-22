using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Moq;

using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Requests;
using Municorn.Infrastructure.Services;

using Xunit;

namespace Municorn.Tests.Infrastructure.UnitTests
{
    public class NotificationCommandHandlerTests
    {
        [Fact]
        public async Task CreateAndroidNotificationCommandHandler_Handle_ReturnSuccess()
        {
            // Arrange
            var sender = new AndroidNotificationSender(new Mock<ILogger<AndroidNotificationSender>>().Object);
            var request = new CreateAndroidNotificationRequest() { DeviceToken = "CreateAndroidNotificationCommandHandler", Message = "Message", Title = "Title" };

            // Act
            var response = await sender.SendNotificationAsync(request);

            // Assert
            Assert.Equal(NotificationStatus.Sent, response);
        }
    }
}
