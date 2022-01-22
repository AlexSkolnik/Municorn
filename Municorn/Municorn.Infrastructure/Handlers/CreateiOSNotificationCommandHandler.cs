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
    public record CreateiOSNotificationCommandHandler : CreateNotificationBaseCommandHandler<CreateiOSNotificationCommand, CreateiOSNotificationRequest>
    {
        private readonly INotificationSender<CreateiOSNotificationRequest> _iOSNotificationSender;

        public CreateiOSNotificationCommandHandler(
            ILogger<CreateiOSNotificationCommandHandler> logger,
            INotificationSender<CreateiOSNotificationRequest> iOSNotificationSender,
            INotificationRepository notificationRepository
            ) : base(logger, notificationRepository)
        {
            _iOSNotificationSender = iOSNotificationSender ?? throw new ArgumentNullException(nameof(iOSNotificationSender));
        }

        public override NotificationDto CreateNotificationDtoFromCommandData(CreateiOSNotificationRequest data)
            => new(data.Id, JsonSerializer.Serialize(data), NotificationType.iOS);

        public override async Task<NotificationStatus> SendNotificationAsync(CreateiOSNotificationRequest data)
            => await _iOSNotificationSender.SendNotificationAsync(data);
    }
}
