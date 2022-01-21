using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Microsoft.Extensions.Logging;

using Municorn.Infrastructure.Commands;
using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Models;
using Municorn.Infrastructure.Responses;

namespace Municorn.Infrastructure.Handlers
{
    public abstract record CreateNotificationBaseCommandHandler<T, U> : IRequestHandler<T, CreateNotificationResponse>
        where T : CreateNotificationBaseCommand<U>
        where U : class, ICreateNotificationRequest
    {
        protected readonly ILogger<CreateNotificationBaseCommandHandler<T, U>> _logger;
        private readonly INotificationRepository _notificaionRepository;

        public CreateNotificationBaseCommandHandler(
            ILogger<CreateNotificationBaseCommandHandler<T, U>> logger,
            INotificationRepository notificationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _notificaionRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
        }

        public virtual async Task<CreateNotificationResponse> Handle(T command, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"[{typeof(T).Name}] -> Handle. Command.Data = '{JsonSerializer.Serialize(command.Data)}'");

            var dto = CreateNotificationDtoFromCommandData(command.Data);
            _logger.LogDebug($"[{typeof(T).Name}] -> CreateNotificationDtoFromCommandData. dto = '{JsonSerializer.Serialize(dto)}'");

            var id = await _notificaionRepository.SaveNotificationAsync(dto);
            _logger.LogDebug($"[{typeof(T).Name}] -> SaveNotificationAsync. id = '{id}'");

            dto.Status = await SendNotificationAsync(command.Data);
            _logger.LogDebug($"[{typeof(T).Name}] -> SendNotificationAsync. status = '{dto.Status}'");

            await _notificaionRepository.UpdateNotificationStatusAsync(dto);
            _logger.LogDebug($"[{typeof(T).Name}] -> UpdateNotificationStatusAsync. dto = '{JsonSerializer.Serialize(dto)}'");

            return new CreateNotificationResponse(id, dto.Status);
        }

        public abstract NotificationDto CreateNotificationDtoFromCommandData(U data);

        public abstract Task<NotificationStatus> SendNotificationAsync(U data);
    }
}