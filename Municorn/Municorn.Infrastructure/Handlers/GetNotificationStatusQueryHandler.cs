using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using Municorn.Infrastructure.Enums;
using Municorn.Infrastructure.Interfaces;
using Municorn.Infrastructure.Queries;

namespace Municorn.Infrastructure.Handlers
{
    public record GetNotificationStatusQueryHandler : IRequestHandler<GetNotificationStatusQuery, NotificationStatus>
    {
        private readonly INotificationRepository _notificaionRepository;

        public GetNotificationStatusQueryHandler(INotificationRepository notificaionRepository)
        {
            _notificaionRepository = notificaionRepository ?? throw new ArgumentNullException(nameof(notificaionRepository));
        }

        public async Task<NotificationStatus> Handle(GetNotificationStatusQuery request, CancellationToken cancellationToken)
            => await _notificaionRepository.GetNotificationStatusByIdAsync(request.Id);
    }
}
