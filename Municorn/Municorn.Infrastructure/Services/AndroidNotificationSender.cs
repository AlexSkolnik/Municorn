using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Requests;

namespace Municorn.Infrastructure.Services
{
    public class AndroidNotificationSender : INotificationSender<CreateAndroidNotificationRequest>
    {
        protected readonly ILogger<AndroidNotificationSender> _logger;

        public AndroidNotificationSender(ILogger<AndroidNotificationSender> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<NotificationStatus> SendNotificationAsync(ICreateNotificationRequest notification)
        {
            try
            {
                var note = (CreateAndroidNotificationRequest)notification;
                _logger.LogWarning($"AndroidNotify -> [{note.Title}]:'{note.Message}'");
                await Task.Delay(1000);
                return NotificationStatus.Sent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка отправки уведомления в android");
                throw;
            }
        }
    }
}
