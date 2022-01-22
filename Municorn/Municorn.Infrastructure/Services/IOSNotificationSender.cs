using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Requests;

namespace Municorn.Infrastructure.Services
{
    public class IOSNotificationSender : INotificationSender<CreateiOSNotificationRequest>
    {
        protected readonly ILogger<IOSNotificationSender> _logger;

        public IOSNotificationSender(ILogger<IOSNotificationSender> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<NotificationStatus> SendNotificationAsync(ICreateNotificationRequest notification)
        {
            try
            {
                var note = (CreateiOSNotificationRequest)notification;
                _logger.LogWarning($"iOSNotify -> '{note.Alert}'");
                await Task.Delay(1000);
                return NotificationStatus.Sent;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка отправки уведомления в ios");
                throw;
            }
        }
    }
}
