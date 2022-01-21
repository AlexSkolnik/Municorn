using MediatR;

using Municorn.Infrastructure.Enums;

namespace Municorn.Infrastructure.Queries
{
    public record GetNotificationStatusQuery(string Id) : IRequest<NotificationStatus>;
}
