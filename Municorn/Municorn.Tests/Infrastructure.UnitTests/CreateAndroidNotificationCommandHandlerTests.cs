using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Moq;

using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Requests;
using Municorn.Infrastructure.Services;

using Xunit;

namespace Municorn.Tests.Infrastructure.UnitTests
{
    public class CreateAndroidNotificationCommandHandlerTests
    {
        [Fact]
        public async Task CreateAndroidNotificationCommandHandler_Handle_ReturnSuccess()
        {
            // Arrange
            var mediatrMock = new AndroidNotificationManager(new Mock<ILogger<AndroidNotificationManager>>().Object);
            var request = new CreateAndroidNotificationRequest() { DeviceToken = "DeviceToken", Message = "Message", Title = "Title" };

            // Act
            var response = await mediatrMock.SendNotificationAsync(request);

            // Assert
            Assert.Equal(NotificationStatus.Sent, response);
        }
    }
}
