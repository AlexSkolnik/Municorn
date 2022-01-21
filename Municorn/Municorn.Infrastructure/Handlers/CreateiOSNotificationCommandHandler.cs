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
        private readonly INotificationManager<CreateiOSNotificationRequest> _iOSNotificationManager;

        public CreateiOSNotificationCommandHandler(
            ILogger<CreateiOSNotificationCommandHandler> logger,
            INotificationManager<CreateiOSNotificationRequest> iOSNotificationManager,
            INotificationRepository notificationRepository
            ) : base(logger, notificationRepository)
        {
            _iOSNotificationManager = iOSNotificationManager ?? throw new ArgumentNullException(nameof(iOSNotificationManager));
        }

        public override NotificationDto CreateNotificationDtoFromCommandData(CreateiOSNotificationRequest data)
            => new(data.Id, JsonSerializer.Serialize(data), NotificationType.iOS);

        public override async Task<NotificationStatus> SendNotificationAsync(CreateiOSNotificationRequest data)
            => await _iOSNotificationManager.SendNotificationAsync(data);
    }
}
