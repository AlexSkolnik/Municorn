using System.Text.Json;
using System.Threading.Tasks;

using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Models;
using Municorn.Infrastructure.Requests;

using Xunit;

namespace Municorn.Tests.Infrastructure.UnitTests
{
    public class NotificationRepositoryTests
    {
        [Fact]
        public async Task SaveNotificationAsync_AddNotification_Success()
        {
            // Arange
            var repository = CommonSut.GetInMemoryNotificationRepository();
            var request = new CreateAndroidNotificationRequest() { DeviceToken = "SaveNotificationAsync_AddNotification_Success", Message = "Message", Title = "Title" };
            var notificationDto = new NotificationDto(request.DeviceToken, JsonSerializer.Serialize(request), NotificationType.Android, NotificationStatus.Sent);

            // Act
            var id = await repository.SaveNotificationAsync(notificationDto);

            // Assert
            Assert.Equal(request.DeviceToken, id);
        }
    }
}
