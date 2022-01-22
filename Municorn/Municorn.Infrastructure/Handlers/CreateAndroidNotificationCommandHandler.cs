using System;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Municorn.Infrastructure.Commands;
using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Models;
using Municorn.Infrastructure.Requests;

namespace Municorn.Infrastructure.Handlers
{
    public record CreateAndroidNotificationCommandHandler : CreateNotificationBaseCommandHandler<CreateAndroidNotificationCommand, CreateAndroidNotificationRequest>
    {
        private readonly INotificationSender<CreateAndroidNotificationRequest> _androidNotificationSender;

        public CreateAndroidNotificationCommandHandler(
            ILogger<CreateAndroidNotificationCommandHandler> logger,
            INotificationSender<CreateAndroidNotificationRequest> androidNotificationSender,
            INotificationRepository notificationRepository
            ) : base(logger, notificationRepository)
        {
            _androidNotificationSender = androidNotificationSender ?? throw new ArgumentNullException(nameof(androidNotificationSender));
        }

        public override NotificationDto CreateNotificationDtoFromCommandData(CreateAndroidNotificationRequest data)
            => new(data.Id, JsonSerializer.Serialize(data), NotificationType.Android);

        public override async Task<NotificationStatus> SendNotificationAsync(CreateAndroidNotificationRequest data)
            => await _androidNotificationSender.SendNotificationAsync(data);
    }
}
